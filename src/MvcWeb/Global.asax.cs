using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Triangles.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			

			routes.MapRoute(
				"Index",
				"",
				new { controller = "Expenditures", action = "Index"}
			);

			routes.MapRoute(
				"Session",
				"{sessionUrl}",
				new { controller = "Expenditures", action = "WorkSession", sessionUrl = UrlParameter.Optional }
			);

			routes.MapRoute(
				"Calculation",
				"Calculation/{action}/{sessionurl}",
				new { controller = "Calculation", action = "CommonFund" }
			);

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Expenditures", action = "Index", id = UrlParameter.Optional }
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}