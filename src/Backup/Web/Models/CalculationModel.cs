using Triangles.Code;
using Triangles.Code.BusinessLogic;

namespace Triangles.Web.Models
{
	public class CalculationModel
	{
		public Transfer[] Transfers { get; set; }
		public string SessionUrl { get; set; }
	}
}