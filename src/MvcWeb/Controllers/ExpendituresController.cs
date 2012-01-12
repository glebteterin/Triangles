using System.Linq;
using System.Web.Mvc;
using Triangles.Code.DataAccess;
using Triangles.Code.Services;
using Triangles.Web.Mappers;
using Triangles.Web.Models;

namespace Triangles.Web.Controllers
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

			return View(GetExpendituresModel(sessionUrl));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Insert(string sessionUrl)
		{
			var expenditure = new Models.Expenditure();
			
			if (TryUpdateModel(expenditure))
			{
				var newExpenditure = ExpenditureMapper.Map(expenditure);
				newExpenditure.SessionId = _sessionService.GetByUrl(sessionUrl).Id;
				_repository.Insert(newExpenditure);

				return RedirectToAction("WorkSession", new { sessionUrl = sessionUrl });
			}

			return View("WorkSession", GetExpendituresModel(sessionUrl));
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
				return RedirectToAction("WorkSession", new { sessionUrl });
			}

			return View("WorkSession", GetExpendituresModel(sessionUrl));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(int id, string sessionUrl)
		{
			_repository.Delete(id);

			return RedirectToAction("WorkSession", new { sessionUrl });
		}

		private ExpendituresModel GetExpendituresModel(string sessionUrl)
		{
			var expenditures = _repository.BySessionUrl(sessionUrl)
									.Select(ExpenditureMapper.Map).ToArray();

			return  new ExpendituresModel { Expenditures = expenditures, SessionUrl = sessionUrl };
		}
	}
}
