using System.Linq;

namespace Triangles.Code.DataAccess
{
	public class ExpenditureRepository
	{
		public void Update(Expenditure expenditure)
		{
			using (var context = new TrianglesDataContext())
			{
				var existedExpenditure = context.Expenditures.First(x => x.Id == expenditure.Id);

				existedExpenditure.Amount = expenditure.Amount;
				existedExpenditure.Description = expenditure.Description;
				existedExpenditure.Who = expenditure.Who;

				context.SubmitChanges();
			}
		}

		public void Insert(Expenditure expenditure)
		{
			using (var context = new TrianglesDataContext())
			{
				context.Expenditures.InsertOnSubmit(expenditure);
				context.SubmitChanges();
			}
		}

		public void Save(Expenditure expenditure)
		{
			using (var context = new TrianglesDataContext())
			{
				var expenditureForUpdate = context.Expenditures.FirstOrDefault(x => x.Id == expenditure.Id);

				expenditureForUpdate.Amount = expenditure.Amount;
				expenditureForUpdate.Description = expenditure.Description;
				expenditureForUpdate.Who = expenditure.Who;

				context.SubmitChanges();
			}
		}

		public void Delete(int id)
		{
			using (var context = new TrianglesDataContext())
			{
				var expenditureToDelete = context.Expenditures.First(x => x.Id == id);
				context.Expenditures.DeleteOnSubmit(expenditureToDelete);
				context.SubmitChanges();
			}
		}

		public Expenditure[] BySessionUrl(string sessionUrl)
		{
			using (var context = new TrianglesDataContext())
			{
				return context.Expenditures.Where(x => x.Session.UniqueUrl == sessionUrl).ToArray();
			}
		}
	}
}