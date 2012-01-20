using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Triangles.Web.Models
{
	[Serializable]
	public class Receipt
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[Required]
		[DisplayName("Кто платил")]
		public string Payer { get; set; }

		[DisplayName("Описание")]
		public string Description { get; set; }

		public ReceiptRecord[] Records { get; set; }
	}
}