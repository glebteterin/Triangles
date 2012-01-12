using System.Collections.Generic;
using System.Linq;

namespace Triangles.Code.BusinessLogic
{
	public class CommonFundCalculator
	{
		private static string FundName = "Общак";

		public Transfer[] Calculate(Expenditure[] expenditures, string[] partners)
		{
			var transfers = CalculateTransfers(expenditures, partners);
			return transfers.ToArray();
		}

		private List<Transfer> CalculateTransfers(Expenditure[] expenditures, string[] partners)
		{
			var transfers = new List<Transfer>();

			var total = expenditures.Sum(x => x.Amount);
			var average = decimal.Round(total / partners.Length, 2);

			foreach (var partner in partners)
			{
				//если этот партнер что-то тратил и он потратил меньше среднего
				//то пусть довнесет разницу
				if (expenditures.Any(x => x.Who == partner))
				{
					var partnerExpenditure = expenditures.First(x => x.Who == partner);

					if (expenditures.First(x => x.Who == partner).Amount < average)
					{
						transfers.Add(new Transfer
									{
										Amount = average - partnerExpenditure.Amount,
										From = partner,
										To = FundName
									});
					}
					else
					{
						//если партнер потратил больше среднего
						//то пусть возмет из общего котла
						transfers.Add(new Transfer
						{
							Amount = partnerExpenditure.Amount - average,
							From = FundName,
							To = partner
						});
					}
				}
				else
				{
					//партнер не тратил ничего, так что пусть вносит среднее
					transfers.Add(new Transfer
					{
						Amount = average,
						From = partner,
						To = FundName
					});
				}
			}

			return transfers;
		}
	}
}