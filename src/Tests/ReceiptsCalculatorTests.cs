using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Triangles.Code.BusinessLogic;
using Triangles.Code.BusinessLogic.Receipt;

namespace Tests
{
	[TestFixture]
	public class ReceiptsCalculatorTests
	{
		[Test]
		public void SimpleCalculation()
		{
			var record11 = new ReceiptRecord("Глеб", 0);
			var record12 = new ReceiptRecord("Данил", 100);
			var record13 = new ReceiptRecord("Альбина", 50);
			var receipt1 = new Receipt {WhoPaid = "Глеб", Records = new[] {record11, record12, record13}};

			var record21 = new ReceiptRecord("Глеб", 10);
			var record22 = new ReceiptRecord("Данил", 260);
			var record23 = new ReceiptRecord("Альбина", 250);
			var receipt2 = new Receipt {WhoPaid = "Данил", Records = new[] {record21, record22, record23}};

			var calculator = new ReceiptsCalculator();
			Transfer[] transactions = calculator.Calculate(new[] { receipt1,receipt2 });

			transactions.First(x => x.To == "Данил").Amount.Should().Be(160);
			transactions.First(x => x.To == "Глеб").Amount.Should().Be(140);
		}
	}
}