using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Triangles.Web.Models
{
	[Serializable]
	public class ReceiptRecord
	{
		[ScaffoldColumn(false)]
		public int Id {get; set;}

		[Required]
		[DisplayName("Сумма")]
		public decimal Amount {get; set;}

		[Required]
		[DisplayName("Участник")]
		public string Participant {get; set;}

		[DisplayName("Описание")]
		public string Description {get; set;}
	}
}