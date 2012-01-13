namespace Triangles.Code.BusinessLogic
{
	/// <summary>
	/// Отражает задолженность партнера перед другими
	/// </summary>
	public class Engagement
	{
		public Engagement(string participant, decimal amount)
		{
			Who = participant;
			Amount = amount;
		}

		public string Who { get; set; }
		public decimal Amount { get; set; }
	}
}