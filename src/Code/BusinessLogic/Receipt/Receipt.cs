namespace Triangles.Code.BusinessLogic.Receipt
{
	/// <summary>
	/// Кассовый чек
	/// </summary>
	public class Receipt
	{
		public ReceiptRecord[] Records { get; set; }
		public string WhoPaid { get; set; }
	}
}