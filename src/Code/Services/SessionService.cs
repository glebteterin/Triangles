using System;
using Triangles.Code.DataAccess;
using Triangles.Code.Utils;

namespace Triangles.Code.Services
{
	public class SessionService
	{
		private const int SessionIdLength = 10;

		readonly SessionRepository _sessionRepository = new SessionRepository();

		public string CreateNewSession()
		{
			var stringGenerator = new RandomStringGenerator();
			string newSessionUrl;

			//Generate unique session url
			var isNotUniqueSessionUrl = true;
			do
			{
				newSessionUrl = stringGenerator.RandomString(SessionIdLength);
				var session = _sessionRepository.GetByUniqueUrl(newSessionUrl);
				isNotUniqueSessionUrl = session != null;
			} while (isNotUniqueSessionUrl);

			var newSession = new Session {UniqueUrl = newSessionUrl, Created = DateTime.Now};
			_sessionRepository.Save(newSession);

			return newSessionUrl;
		}

		public Session GetByUrl(string uniqueUrl)
		{
			var session = _sessionRepository.GetByUniqueUrl(uniqueUrl);
			return session;
		}
	}
}