using System;
using System.Collections.Generic;
using System.Linq;

namespace Triangles.Code.BusinessLogic.Receipt
{
	public class ReceiptsCalculator
	{
		public Transfer[] Calculate(Receipt[] receipts)
		{
			var statuses = DecomposeToStatuses(receipts);
			var credits = statuses.Where(x => x.Real > x.Expected).Select(x => new Credit(x.Participant, x.Real - x.Expected)).ToList();
			var engagements = statuses.Where(x => x.Expected > x.Real).Select(x => new Engagement(x.Participant, x.Expected - x.Real)).ToList();

			var result = Calculate(credits, engagements).ToArray();
			return result;
		}

		private ParticipantStatus[] DecomposeToStatuses(IEnumerable<Receipt> receipts)
		{
			var statuses = new List<ParticipantStatus>();

			foreach (var receipt in receipts)
			{
				ParticipantStatus payer;

				//1. обрабатываем плательщика
				if (!statuses.Any(x => x.Participant == receipt.Payer))
				{
					payer = new ParticipantStatus
					{
						Participant = receipt.Payer,
						Real = 0,
						Expected = 0,
					};

					statuses.Add(payer);
				}
				else
				{
					payer = statuses.Single(x => x.Participant == receipt.Payer);
				}

				if (receipt.Records.Any(x => x.Participant == payer.Participant))
				{
					//если плательщик покупал товары тоже
					//то потратил он сумму чека - свои покупки
					payer.Real += receipt.Records.Sum(x => x.Amount) - receipt.Records.Where(x => x.Participant == receipt.Payer).Sum(x => x.Amount);
					//а ожидал потратить 0 (потому что мы уже учли это в реальных затратах)
				}
				else
				{
					//если плательщик покупал не покупал товары в этом чеке
					//то потратил он сумму чека
					payer.Real += receipt.Records.Sum(x => x.Amount);
					//а ожидал потратить 0
				}

				//2. обрабатываем участников
				foreach (var receiptRecord in receipt.Records)
				{
					//добавляем в статусы нового участника, если его еще нет
					if (!statuses.Any(x => x.Participant == receiptRecord.Participant))
						statuses.Add(new ParticipantStatus {Participant = receiptRecord.Participant});

					var status = statuses.Single(x => x.Participant == receiptRecord.Participant);
					if (status.Participant != receipt.Payer)
						status.Expected += receipt.Records.Single(x => x.Participant == status.Participant).Amount;
				}
			}

			return statuses.ToArray();
		}

		/// <param name="credits">Задолженности других участников перед участником</param>
		/// <param name="engagements">Задолженности участника перед другими участниками</param>
		private List<Transfer> Calculate(List<Credit> credits, List<Engagement> engagements)
		{
			var transfers = new List<Transfer>();

			while (credits.Any())
			{
				//1. Проверяем наличие совпадающих сумм, чтобы взаимопогасить их
				CancelSameAmount(credits, engagements, transfers);

				if (!credits.Any())
				{
					break;
				}

				//2. Сравниваем максимальный дебит (максД) и кредит (максК)
				//   Если максД > максК то частично гасим максД из максК (максК берется полностью)
				//   Если максК > максД то полностью гасим максД из максК (максК берется частично)
				var maxEngagement = engagements.OrderByDescending(x => x.Amount).Take(1).FirstOrDefault();
				var maxCredit = credits.OrderByDescending(x => x.Amount).Take(1).FirstOrDefault();

				if (maxCredit.Amount > maxEngagement.Amount)
				{
					maxCredit.Amount -= maxEngagement.Amount;
					transfers.Add(new Transfer { Amount = maxEngagement.Amount, To = maxCredit.Who, From = maxEngagement.Who });
					engagements.Remove(maxEngagement);
				}
				else
				{
					maxEngagement.Amount -= maxCredit.Amount;
					transfers.Add(new Transfer { Amount = maxCredit.Amount, To = maxCredit.Who, From = maxEngagement.Who });
					credits.Remove(maxCredit);
				}
			}

			return transfers;
		}

		/// <summary>
		/// Гасит дебиты и кредиты с равными суммами и переводит их в платежи.
		/// </summary>
		/// <param name="credits">Задолженности других участников перед участником</param>
		/// <param name="engagements">Задолженности участника перед другими участниками</param>
		/// <param name="transfers"></param>
		private static void CancelSameAmount(List<Credit> credits, List<Engagement> engagements, List<Transfer> transfers)
		{
			bool stop = false;
			while (!stop)
			{
				stop = true;

				//вся эта возня с модулем нужна чтобы иметь возможность гасить 60 и 61 копейку
				var creditToDelete = credits.FirstOrDefault(c => engagements.Any(e => Math.Abs(e.Amount - c.Amount) < 1));

				if (creditToDelete != null)
				{
					var engagementToDelete = engagements.First(e => Math.Abs(e.Amount - creditToDelete.Amount) < 1);

					transfers.Add(new Transfer
					{
						From = engagementToDelete.Who,
						To = creditToDelete.Who,
						Amount = Math.Max(creditToDelete.Amount, engagementToDelete.Amount)
					});

					engagements.Remove(engagementToDelete);
					credits.Remove(creditToDelete);

					stop = false;
				}
			}
		}
	}
}