using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Triangles.Code;
using Triangles.Code.BusinessLogic;

namespace Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Test()
        {
            var albina = new Expenditure{Amount = 200, Who = "Глеб"};
            var danil = new Expenditure{Amount = 100, Who = "Альбина"};
			var gleb = new Expenditure { Amount = 9, Who = "Данил" };

            var calculator = new FlowsCalculator();
			var transactions = calculator.Calculate(new[] { albina, gleb, danil }, new[] { "Глеб", "Альбина", "Данил" });

            transactions.First(x => x.From == "Данил").Amount.Should().Be(94);
			transactions.First(x => x.From == "Альбина").Amount.Should().Be(3);
        }

        [Test]
        public void Test2()
        {
			var albina = new Expenditure { Amount = 200, Who = "Глеб" };
			var danil = new Expenditure { Amount = 674, Who = "Альбина" };
			var gleb = new Expenditure { Amount = 500, Who = "Данил" };
			var sasha = new Expenditure { Amount = 13, Who = "Санек" };

            var calculator = new FlowsCalculator();
			var transactions = calculator.Calculate(new[] { albina, gleb, danil, sasha }, new[] { "Глеб", "Альбина", "Данил", "Санек" });

			transactions.First(x => (x.From == "Санек" && x.To == "Альбина")).Amount.Should().Be((decimal)327.25);
			transactions.First(x => (x.From == "Глеб" && x.To == "Данил")).Amount.Should().Be((decimal)146.75);
			transactions.First(x => (x.From == "Санек" && x.To == "Данил")).Amount.Should().Be((decimal)6.5);
        }
    }
}
