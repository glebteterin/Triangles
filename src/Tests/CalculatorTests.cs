using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Triangles.Code;

namespace Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Test()
        {
            var albina = new Expenditure{Amount = 200, Who = 1};
            var danil = new Expenditure{Amount = 100, Who = 2};
            var gleb = new Expenditure{Amount = 9, Who = 3};

            var calculator = new Calculator();
            var transactions = calculator.CalculateTransfers(new[] {albina, gleb, danil}, new[] {1, 2, 3});

            transactions.First(x => x.From == 3).Amount.Should().Be(94);
            transactions.First(x => x.From == 2).Amount.Should().Be(3);
        }

        [Test]
        public void Test2()
        {
            var albina = new Expenditure { Amount = 200, Who = 1 };
            var danil = new Expenditure { Amount = 674, Who = 2 };
            var gleb = new Expenditure { Amount = 500, Who = 3 };
            var sasha = new Expenditure { Amount = 13, Who = 4 };

            var calculator = new Calculator();
            var transactions = calculator.CalculateTransfers(new[] { albina, gleb, danil, sasha }, new[] { 1, 2, 3, 4 });

            transactions.First(x => (x.From == 4 && x.To == 2)).Amount.Should().Be((decimal)327.25);
            transactions.First(x => (x.From == 1 && x.To == 3)).Amount.Should().Be((decimal)146.75);
            transactions.First(x => (x.From == 4 && x.To == 3)).Amount.Should().Be((decimal)6.5);
        }
    }
}
