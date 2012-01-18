using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using Triangles.Code.DataAccess;
using Triangles.Code.Services;
using Triangles.Web.Mappers;
using Triangles.Web.Models;
using Expenditure = Triangles.Web.Models.Expenditure;

namespace Triangles.Web.Controllers
{
	public class ExpendituresController : Controller
	{
		readonly ExpenditureRepository _repository = new ExpenditureRepository();
		readonly SessionService _sessionService = new SessionService();

		public ActionResult Show(string sessionUrl)
		{
			return View(new ExpendituresModel{Expenditures = GetExpenditures(sessionUrl), SessionUrl = sessionUrl});
		}

		[GridAction]
		public ActionResult AjaxSelect(string sessionUrl)
		{
			return View(new GridModel(GetExpenditures(sessionUrl)));
		}

		[GridAction]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult AjaxInsert(string sessionUrl)
		{
			var expenditure = new Models.Expenditure();

			if (TryUpdateModel(expenditure) && ValidateParticipantDuplicating(expenditure, sessionUrl, ModelState))
			{
				var newExpenditure = ExpenditureMapper.Map(expenditure);
				newExpenditure.SessionId = _sessionService.GetByUrl(sessionUrl).Id;
				_repository.Insert(newExpenditure);
			}

			return View(new GridModel(GetExpenditures(sessionUrl)));
		}

		[GridAction]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult AjaxSave(int id, string sessionUrl)
		{
			var expenditure = new Models.Expenditure { Id = id };

			if (TryUpdateModel(expenditure) && ValidateParticipantDuplicating(expenditure, sessionUrl, ModelState))
			{
				_repository.Save(ExpenditureMapper.Map(expenditure));
			}

			return View(new GridModel(GetExpenditures(sessionUrl)));
		}

		[GridAction]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult AjaxDelete(int id, string sessionUrl)
		{
			_repository.Delete(id);

			return View(new GridModel(GetExpenditures(sessionUrl)));
		}

		private Expenditure[] GetExpenditures(string sessionUrl)
		{
			return _repository.BySessionUrl(sessionUrl)
									.Select(ExpenditureMapper.Map).ToArray();
		}

		private bool ValidateParticipantDuplicating(Models.Expenditure expenditure, string sessionUrl, ModelStateDictionary modelState)
		{
			var existedExpenditures = _sessionService.GetByUrl(sessionUrl).Expenditures;
			if (existedExpenditures.Any(x=>x.Who.Trim().ToLower() == expenditure.Who.Trim().ToLower() && x.Id != expenditure.Id))
			{
				modelState.AddModelError("Who","Такой участник уже существует");
				return false;
			}
			return true;
		}
	}
}
