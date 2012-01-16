using System.Linq;

namespace Triangles.Code.DataAccess
{
	public class ReceiptRepository
	{
		public void Insert(Receipt receipt)
		{
			using (var context = new TrianglesDataContext())
			{
				context.Receipts.InsertOnSubmit(receipt);
				context.SubmitChanges();
			}
		}

		public void Update(Receipt receipt)
		{
			using (var context = new TrianglesDataContext())
			{
				var existedReceipt = context.Receipts.First(x => x.Id == receipt.Id);

				existedReceipt.Description = receipt.Description;
				existedReceipt.Payer = receipt.Payer;

				context.SubmitChanges();
			}
		}

		public void Delete(int id)
		{
			using (var context = new TrianglesDataContext())
			{
				var receiptToDelete = context.Receipts.First(x => x.Id == id);
				context.Receipts.DeleteOnSubmit(receiptToDelete);
				context.SubmitChanges();
			}
		}

		public Receipt[] BySessionUrl(string sessionUrl)
		{
			using (var context = new TrianglesDataContext())
			{
				return context.Receipts.Where(x => x.Session.UniqueUrl == sessionUrl).ToArray();
			}
		}
	}
}