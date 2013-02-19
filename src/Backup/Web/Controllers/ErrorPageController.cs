using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triangles.Web.Controllers
{
	public class ErrorPageController : Controller
	{
		public ActionResult Error(string errorCode)
		{
			int code = 0;
			int.TryParse(errorCode, out code);

			switch (code)
			{
				case 403:
					return View("403");
				case 404:
					return View("404");
				default:
					return View("500");
			}
		}
	}
}
