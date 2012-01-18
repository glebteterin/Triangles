using System.Data.Linq;
using System.Linq;

namespace Triangles.Code.DataAccess
{
	public class SessionRepository
	{
		public void Save(Session session)
		{
			using (var context = new TrianglesDataContext())
			{
				context.Sessions.InsertOnSubmit(session);
				context.SubmitChanges();
			}
		}

		public Session GetByUniqueUrl(string sessionUrl)
		{
			using (var context = new TrianglesDataContext())
			{
				var options = new DataLoadOptions();
				options.LoadWith<Session>(s => s.Expenditures);
				options.LoadWith<Session>(s => s.Receipts);
				context.LoadOptions = options;

				var session = context.Sessions.FirstOrDefault(x => x.UniqueUrl == sessionUrl);
				return session;
			}
		}
	}
}