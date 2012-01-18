using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Telerik.Web.Mvc;
using Triangles.Code.Services;
using Triangles.Web.Mappers;
using Triangles.Web.Models;
using Triangles.Code.DataAccess;
using Receipt = Triangles.Web.Models.Receipt;

namespace Triangles.Web.Controllers
{
	public class ReceiptsController : Controller
	{
		readonly SessionService _sessionService = new SessionService();
		readonly ReceiptRepository _repository = new ReceiptRepository();

		public ActionResult Show(string sessionUrl)
		{
			return View(new ReceiptsModel {Receipts = _sessionService.GetByUrl(sessionUrl).Receipts.Select(x=>Mapper.Map<Receipt>(x)).ToArray()});
		}

		[GridAction]
		public ActionResult AjaxSelect(string sessionUrl)
		{
			return View(new GridModel(_sessionService.GetByUrl(sessionUrl).Receipts.Select(x => Mapper.Map<Receipt>(x)).ToArray()));
		}

		[GridAction]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult AjaxInsert(string sessionUrl)
		{
			var receipt = new Models.Receipt();

			if (TryUpdateModel(receipt))
			{
				var newExpenditure = Mapper.Map<Code.DataAccess.Receipt>(receipt);
				newExpenditure.SessionId = _sessionService.GetByUrl(sessionUrl).Id;
				_repository.Insert(newExpenditure);
			}

			return View(new GridModel(_sessionService.GetByUrl(sessionUrl).Receipts.Select(x => Mapper.Map<Receipt>(x)).ToArray()));
		}

		[GridAction]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult AjaxSave(int id, string sessionUrl)
		{
			var receipt = new Models.Receipt { Id = id };

			if (TryUpdateModel(receipt))
			{
				_repository.Update(Mapper.Map<Code.DataAccess.Receipt>(receipt));
			}

			return View(new GridModel(_sessionService.GetByUrl(sessionUrl).Receipts.Select(x => Mapper.Map<Receipt>(x)).ToArray()));
		}

		[GridAction]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult AjaxDelete(int id, string sessionUrl)
		{
			_repository.Delete(id);

			return View(new GridModel(_sessionService.GetByUrl(sessionUrl).Receipts.Select(x => Mapper.Map<Receipt>(x)).ToArray()));
		}
	}
}
