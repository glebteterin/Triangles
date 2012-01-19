using System.Linq;
using System.Web;

namespace Triangles.Code.Tools.Web
{
	public class CookieManager
	{
		private readonly HttpContextBase _context;

		const string SessionCookieName = "session";

		public CookieManager(HttpContextBase context)
		{
			_context = context;
		}

		public bool IsUserFirstTimeEnter()
		{
			return !_context.Request.Cookies.AllKeys.Any(x => x == SessionCookieName);
		}

		public string LoadSessionUrl()
		{
			if (IsUserFirstTimeEnter())
				return string.Empty;

			var result = _context.Request.Cookies[SessionCookieName].Value;
			return result;
		}

		public void SaveUserSessionUrl(string sessionUrl)
		{
			_context.Response.Cookies.Add(new HttpCookie(SessionCookieName, sessionUrl));
		}
	}
}