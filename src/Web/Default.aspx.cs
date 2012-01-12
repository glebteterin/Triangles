using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Triangles.Code.DataAccess;

namespace Triangles.Web
{
	public partial class Default : System.Web.UI.Page
	{
		readonly ExpenditureRepository _repository = new ExpenditureRepository();

		protected void Page_Load(object sender, EventArgs e)
		{
			
		}

		protected void rgExpenditures_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			rgExpenditures.DataSource = _repository.All();
		}

		protected void rgExpenditures_UpdateCommand(object source, GridCommandEventArgs e)
		{
			UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

			var expenditure = new Expenditure();
			expenditure.Id = int.Parse((userControl.FindControl("txtId") as TextBox).Text);
			expenditure.Who = (userControl.FindControl("txtWho") as TextBox).Text;
			expenditure.Description = (userControl.FindControl("txtDescription") as TextBox).Text;
			expenditure.Amount = decimal.Parse((userControl.FindControl("txtAmount") as TextBox).Text);

			_repository.Update(expenditure);
		}

		protected void rgExpenditures_InsertCommand(object source, GridCommandEventArgs e)
		{
			UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

			var expenditure = new Expenditure();
			expenditure.Who = (userControl.FindControl("txtWho") as TextBox).Text;
			expenditure.Description = (userControl.FindControl("txtDescription") as TextBox).Text;
			expenditure.Amount = decimal.Parse((userControl.FindControl("txtAmount") as TextBox).Text);

			_repository.Save(expenditure);
		}

		protected void rgExpenditures_DeleteCommand(object sender, GridCommandEventArgs e)
		{
			var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");

			_repository.Delete(id);
		}

		protected void btnCalculate_Click(object sender, EventArgs e)
		{
			Response.Redirect("/CalculationResult.aspx");
		}

		protected void btnCalculateWithCommontFund_Click(object sender, EventArgs e)
		{
			Response.Redirect("/CalculationWithCommonFundResult.aspx");
		}
	}
}