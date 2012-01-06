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
	}
}