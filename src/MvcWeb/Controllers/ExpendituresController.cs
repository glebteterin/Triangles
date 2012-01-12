using System.Linq;
using System.Web.Mvc;
using MvcWeb.Mappers;
using MvcWeb.Models;
using Triangles.Code.DataAccess;
using Triangles.Code.Services;

namespace MvcWeb.Controllers
{
	public class ExpendituresController : Controller
	{
		readonly ExpenditureRepository _repository = new ExpenditureRepository();
		readonly SessionService _sessionService = new SessionService();

		public ActionResult Index()
		{
			string sessionUrl;

			if (Session["sessionurl"] != null)
				sessionUrl = Session["sessionurl"] as string;
			else
				return NewSession();

			return RedirectToAction("WorkSession", new {sessionUrl });
		}

		public ActionResult NewSession()
		{
			var sessionUrl = _sessionService.CreateNewSession();
			
			return RedirectToAction("WorkSession", new {sessionUrl });
		}

		public ActionResult WorkSession(string sessionUrl)
		{
			Session["sessionurl"] = sessionUrl;

			if (_sessionService.GetByUrl(sessionUrl) == null)
				return NewSession();

			var expenditures = _repository.BySessionUrl(sessionUrl)
									.Select(ExpenditureMapper.Map).ToArray();

			return View(new ExpendituresModel { Expenditures = expenditures, SessionUrl = sessionUrl });
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Insert(string sessionUrl)
		{
			var expenditure = new Models.Expenditure();
			
			if (TryUpdateModel(expenditure))
			{
				var newExpenditure = ExpenditureMapper.Map(expenditure);
				newExpenditure.Session = _sessionService.GetByUrl(sessionUrl);
				_repository.Insert(newExpenditure);
			}

			return RedirectToAction("WorkSession", new { sessionUrl = sessionUrl });
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Save(int id, string sessionUrl)
		{
			var expenditure = new Models.Expenditure
			{
				Id = id
			};

			if (TryUpdateModel(expenditure))
			{
				_repository.Save(ExpenditureMapper.Map(expenditure));
			}

			return RedirectToAction("WorkSession", new { sessionUrl = sessionUrl });
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(int id, string sessionUrl)
		{
			_repository.Delete(id);

			return RedirectToAction("WorkSession", new { sessionUrl = sessionUrl });
		}

	}
}
