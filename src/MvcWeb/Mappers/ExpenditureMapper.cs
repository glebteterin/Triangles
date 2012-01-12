using Triangles.Code.DataAccess;

namespace MvcWeb.Mappers
{
	public class ExpenditureMapper
	{
		public static Models.Expenditure Map(Expenditure source)
		{
			var dest = new Models.Expenditure
							{
								Who = source.Who, 
								Amount = source.Amount, 
								Description = source.Description,
								Id = source.Id
							};

			return dest;
		}

		public static Expenditure Map(Models.Expenditure source)
		{
			var dest = new Expenditure
			{
				Who = source.Who,
				Amount = source.Amount,
				Description = source.Description,
				Id = source.Id
			};

			return dest;
		}
	}
}