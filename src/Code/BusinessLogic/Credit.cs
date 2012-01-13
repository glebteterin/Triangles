namespace Triangles.Code.BusinessLogic
{
	/// <summary>
	/// Отражает задолженность других партнеров перед партнером
	/// </summary>
	public class Credit
	{
		public string Who { get; set; }
		public decimal Amount { get; set; }

		public Credit(string who, decimal amount)
		{
			Who = who;
			Amount = amount;
		}
	}
}