using System.Linq;
using System.Web;

namespace Triangles.Code.Tools.Web
{
	public class CookieManager
	{
		private readonly HttpContextBase _context;

		const string FirstTimeCookieName = "ft";

		public CookieManager(HttpContextBase context)
		{
			_context = context;
		}

		public void SaveUserWasHere()
		{
			_context.Response.Cookies.Add(new HttpCookie(FirstTimeCookieName, "True"));
		}

		public bool IsUserFirstTimeEnter()
		{
			return !_context.Request.Cookies.AllKeys.Any(x => x == FirstTimeCookieName);
		}
	}
}