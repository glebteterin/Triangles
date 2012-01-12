using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triangles.Code;
using Triangles.Code.BusinessLogic;
using Triangles.Code.DataAccess;
using Triangles.Web.Models;
using Expenditure = Triangles.Code.BusinessLogic.Expenditure;

namespace Triangles.Web.Controllers
{
	public class CalculationController : Controller
	{
		readonly SessionRepository _sessionRepository = new SessionRepository();
		readonly CommonFundCalculator _commonFundCalculator = new CommonFundCalculator();
		readonly FlowsCalculator _flowFlowsCalculator = new FlowsCalculator();

		public ActionResult CommonFund(string sessionUrl)
		{
			var session = _sessionRepository.GetByUniqueUrl(sessionUrl);

			var transfers = 
				_commonFundCalculator.Calculate(
					session.Expenditures.Select(x=>new Expenditure{Amount = x.Amount, Who = x.Who}).ToArray(), 
					session.Expenditures.Select(x => x.Who).Distinct().ToArray());

			return View(new CalculationModel {Transfers = transfers, SessionUrl = sessionUrl});
		}

		public ActionResult Flows(string sessionUrl)
		{
			var session = _sessionRepository.GetByUniqueUrl(sessionUrl);

			var transfers =
				_flowFlowsCalculator.Calculate(
					session.Expenditures.Select(x => new Expenditure { Amount = x.Amount, Who = x.Who }).ToArray(),
					session.Expenditures.Select(x => x.Who).Distinct().ToArray());

			return View(new CalculationModel { Transfers = transfers, SessionUrl = sessionUrl });
		}
	}
}
