using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Triangles.Code.DataAccess;

namespace Triangles.Web
{
	public partial class ExpenditureEditForm : System.Web.UI.UserControl
	{
		private object _dataItem = null;

		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		private void RecordDetails_DataBinding(object sender, EventArgs e)
		{
		}

		/// <summary>
		///Required method for Designer support - do not modify
		///the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.DataBinding += new System.EventHandler(this.RecordDetails_DataBinding);
		}
	}
}