using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Triangles.Code.Services;

namespace Triangles.Web.Models
{
	[Serializable]
	public class Expenditure
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }
		[Required]
		[DisplayName("Кто")]
		public string Who { get; set; }
		[Required]
		[DisplayName("Сумма")]
		public decimal Amount { get; set; }
		[DisplayName("Описание")]
		public string Description { get; set; }
	}
}