using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triangles.Code.Services;
using Triangles.Code.Tools.Web;
using Triangles.Web.Models;

namespace Triangles.Web.Controllers
{
	public class HomeController : Controller
	{
		readonly SessionService _sessionService = new SessionService();
		CookieManager _cookieManager;

		public ActionResult Index()
		{
			string sessionUrl;

			if (Session["sessionurl"] != null)
				sessionUrl = Session["sessionurl"] as string;
			else
				return NewSession();

			return RedirectToAction("WorkSession", new { sessionUrl });
		}

		public ActionResult NewSession()
		{
			var sessionUrl = _sessionService.CreateNewSession();

			return RedirectToAction("WorkSession", new { sessionUrl });
		}

		public ActionResult WorkSession(string sessionUrl)
		{
			_cookieManager = new CookieManager(this.HttpContext);

			Session["sessionurl"] = sessionUrl;

			if (_sessionService.GetByUrl(sessionUrl) == null)
				return NewSession();

			var model = new HomeModel {IsFirstEnter = _cookieManager.IsUserFirstTimeEnter(), SessionUrl = sessionUrl};
			_cookieManager.SaveUserWasHere();

			return View(model);
		}
	}
}
