using System.Linq;

namespace Triangles.Code.DataAccess
{
	public class ExpenditureRepository
	{
		public Expenditure[] All()
		{
			using (var context = new TrianglesDataContext())
			{
				return context.Expenditures.ToArray();
			}
		}

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

		public void Save(Expenditure expenditure)
		{
			using (var context = new TrianglesDataContext())
			{
				context.Expenditures.InsertOnSubmit(expenditure);
				context.SubmitChanges();
			}
		}
	}
}