using System.Collections.Generic;
using System.Linq;

namespace Triangles.Code
{
    public class Calculator
    {
        public Transfer[] CalculateTransfers(Expenditure[] expenditures, int[] partners)
        {
            var engagements = CalculateEngagements(expenditures, partners);
            var credits = CalculateCredits(expenditures, partners);

            var transfers = CalculateTransfers(engagements, credits);
            return transfers.ToArray();
        }

        private List<Transfer> CalculateTransfers(List<Engagement> engagements, List<Credit> credits)
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

                var creditToDelete = credits.FirstOrDefault(c => engagements.Any(e => e.Amount == c.Amount));

                if (creditToDelete != null)
                {
                    var engagementToDelete = engagements.First(e => e.Amount == creditToDelete.Amount);

                    transfers.Add(new Transfer
                                      {
                                          From = engagementToDelete.Who,
                                          To = creditToDelete.Who,
                                          Amount = creditToDelete.Amount
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
        private List<Engagement> CalculateEngagements(IEnumerable<Expenditure> expenditures, int[] partners)
        {
            var total = expenditures.Sum(x => x.Amount);
            var average = total/partners.Length;

            var engagements = new List<Engagement>();

            foreach (var partner in partners)
            {
                var partnerExpenditure = expenditures.FirstOrDefault(x => x.Who == partner);
                
                if (partnerExpenditure == null)
                    //партнер вообще ничего не тратил и должен выплатить среднее
                    engagements.Add(new Engagement { Amount = average, Who = partner });
                else if (partnerExpenditure.Amount < average)
                    //партнер потратил меньше среднего и должен доплатить
                    engagements.Add(new Engagement { Amount = average - partnerExpenditure.Amount, Who = partner});
            }

            return engagements;
        }

        /// <summary>
        /// Возвращает кредиторские задолженности
        /// </summary>
        private List<Credit> CalculateCredits(IEnumerable<Expenditure> expenditures, int[] partners)
        {
            var total = expenditures.Sum(x => x.Amount);
            var average = total / partners.Length;

            var credits = new List<Credit>();

            foreach (var partner in partners)
            {
                var partnerExpenditure = expenditures.FirstOrDefault(x => x.Who == partner);

                if (partnerExpenditure != null && partnerExpenditure.Amount > average)
                    //партнер заплатил больше среднего
                    credits.Add(new Credit { Amount = partnerExpenditure.Amount - average, Who = partner});
            }

            return credits;
        }
    }
}