using System;
using System.Collections.Generic;
using System.Linq;

namespace Triangles.Code.BusinessLogic
{
	public class FlowsCalculator
	{
		public Transfer[] Calculate(Expenditure[] expenditures, string[] partners)
		{
			var engagements = CalculateEngagements(expenditures, partners);
			var credits = CalculateCredits(expenditures, partners);

			var transfers = Calculate(engagements, credits);
			return transfers.ToArray();
		}

		private List<Transfer> Calculate(List<Engagement> engagements, List<Credit> credits)
		{
			var transfers = new List<Transfer>();

			while (credits.Any())
			{
				//1. Проверяем наличие совпадающих сумм, чтобы взаимопогасить их
				CancelSameAmount(engagements, credits, transfers);

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
		private static void CancelSameAmount(List<Engagement> engagements, List<Credit> credits, List<Transfer> transfers)
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


		/// <summary>
		/// Возвращает дебиторские задолженности
		/// </summary>
		private List<Engagement> CalculateEngagements(IEnumerable<Expenditure> expenditures, string[] partners)
		{
			decimal total;
			decimal average;
			CalculateAverageAndTotal(expenditures, partners, out total, out average);

			var engagements = new List<Engagement>();

			foreach (var partner in partners)
			{
				var partnerExpenditure = expenditures.FirstOrDefault(x => x.Who == partner);

				if (partnerExpenditure == null || partnerExpenditure.Amount == 0)
					//партнер вообще ничего не тратил и должен выплатить среднее
					engagements.Add(new Engagement(partner, average));
				else if (partnerExpenditure.Amount < average)
					//партнер потратил меньше среднего и должен доплатить
					engagements.Add(new Engagement(partner, average - partnerExpenditure.Amount));
			}

			return engagements;
		}

		/// <summary>
		/// Возвращает кредиторские задолженности
		/// </summary>
		private List<Credit> CalculateCredits(IEnumerable<Expenditure> expenditures, string[] partners)
		{
			decimal total;
			decimal average;
			CalculateAverageAndTotal(expenditures, partners, out total, out average);

			var credits = new List<Credit>();

			foreach (var partner in partners)
			{
				var partnerExpenditure = expenditures.FirstOrDefault(x => x.Who == partner);

				if (partnerExpenditure != null && partnerExpenditure.Amount > average)
					//партнер заплатил больше среднего
					credits.Add(new Credit(partner, partnerExpenditure.Amount - average));
			}

			return credits;
		}

		private static void CalculateAverageAndTotal(IEnumerable<Expenditure> expenditures, string[] partners, out decimal total, out decimal average)
		{
			total = expenditures.Sum(x => x.Amount);
			average = decimal.Round(total / partners.Length, 2);
		}
	}
}