using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Triangles.Code;
using Triangles.Code.BusinessLogic;
using Triangles.Code.DataAccess;
using Expenditure = Triangles.Code.BusinessLogic.Expenditure;

namespace Triangles.Web
{
	public partial class CalculationResult : System.Web.UI.Page
	{
		readonly ExpenditureRepository _repository = new ExpenditureRepository();
		readonly FlowsCalculator _flowsCalculator = new FlowsCalculator();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var records = _repository.All();

				lvRecords.DataSource = records;
				lvRecords.DataBind();

				var transfers =_flowsCalculator.Calculate(
															records.Select(x => new Expenditure {Who = x.Who, Amount = x.Amount}).ToArray(),
															records.Select(x => x.Who).ToArray());

				lvTransfers.DataSource = transfers;
				lvTransfers.DataBind();
			}
		}
	}
}