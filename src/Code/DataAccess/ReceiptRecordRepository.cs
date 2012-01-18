using System.Linq;

namespace Triangles.Code.DataAccess
{
	public class ReceiptRecordRepository
	{
		public void Update(ReceiptRecord receiptRecord)
		{
			using (var context = new TrianglesDataContext())
			{
				var existedReceiptRecord = context.ReceiptRecords.First(x => x.Id == receiptRecord.Id);

				existedReceiptRecord.Participant = receiptRecord.Participant;
				existedReceiptRecord.Description = receiptRecord.Description;
				existedReceiptRecord.Amount = receiptRecord.Amount;

				context.SubmitChanges();
			}
		}

		public void Delete(int receiptRecordId)
		{
			using (var context = new TrianglesDataContext())
			{
				var receiptRecordToDelete = context.ReceiptRecords.First(x => x.Id == receiptRecordId);
				context.ReceiptRecords.DeleteOnSubmit(receiptRecordToDelete);
				context.SubmitChanges();
			}
		}

		public void Insert(ReceiptRecord receiptRecord)
		{
			using (var context = new TrianglesDataContext())
			{
				context.ReceiptRecords.InsertOnSubmit(receiptRecord);
				context.SubmitChanges();
			}
		}
	}
}