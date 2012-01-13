namespace Triangles.Code.BusinessLogic.Receipt
{
	/// <summary>
	/// Соотношение фактических затрат и реальных
	/// </summary>
	public class ParticipantStatus
	{
		/// <summary>
		/// Реально сумма расходов
		/// </summary>
		public decimal Real { get; set; }
		/// <summary>
		/// Ожидаемая сумма расходом
		/// </summary>
		public decimal Expected { get; set; }

		public string Participant { get; set; }
	}
}