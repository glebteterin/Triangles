namespace Triangles.Code.BusinessLogic.Receipt
{
	/// <summary>
	/// Строка в <see cref="Receipt"/>
	/// </summary>
	public class ReceiptRecord
	{
		public string Participant { get; set; }
		public decimal Amount { get; set; }

		public ReceiptRecord(string participant, decimal amount)
		{
			Participant = participant;
			Amount = amount;
		}
	}
}