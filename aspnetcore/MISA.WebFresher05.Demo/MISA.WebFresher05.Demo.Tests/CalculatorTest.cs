using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace MISA.WebFresher05.Demo.Tests
{
    [TestFixture]
    public class CalculatorTest
    {
        private Calculator _calculator;
        [SetUp]   
        public void SetUp()
        {
            _calculator = new Calculator();
        }


        [TestCase(1, 2, 3)]
        [TestCase(2, 3, 5)]
        [TestCase(int.MaxValue, 4, int.MaxValue + (long)4)]
        public void Add_ValidInput_ValidResult(int x, int y, long expectedResult)
        {
            // Arrange

            // Act
            var actualResult = _calculator.Add(x, y);
            //Assert
            Assert.That(expectedResult, Is.EqualTo(actualResult));

        }


        [TestCase(1, 2, -1)]
        [TestCase(2, 3, -1)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue - (long)int.MinValue)]
        public void Sub_ValidInput_ValidResult(int x, int y, long expectedResult)
        {
            // Arrange

            // Act
            var actualResult = _calculator.Sub(x, y);
            //Assert
            Assert.That(expectedResult, Is.EqualTo(actualResult));

        }


        [TestCase(1, 2, 2)]
        [TestCase(2, 3, 6)]
        [TestCase(2, -3, -6)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue * (long)int.MinValue)]
        public void Mul_ValidInput_ValidResult(int x, int y, long expectedResult)
        {
            // Arrange

            // Act
            var actualResult = _calculator.Mul(x, y);
            //Assert
            Assert.That(expectedResult, Is.EqualTo(actualResult));

        }

        [Test]
        public void Div_DivideByZero_ReturnException()
        {
            //Arrange
            var x = 1;
            var y = 0;
            var expectedExceptionMessage = "Không chia được cho 0";
            //Act
            //Assert
            var exception = Assert.Throws<Exception>(() => _calculator.Div(x, y));

            Assert.That(exception.Message, Is.EqualTo(expectedExceptionMessage));
        }



        [TestCase(1, 2, 0.5)]
        [TestCase(2, 3, 0.6666)]
        public void Div_ValidInput_ValidResult(int x, int y, double expectedResult)
        {
            // Act
            var actualResult = _calculator.Div(x, y);

            Assert.That(Math.Abs(actualResult - expectedResult), Is.LessThan(1e-3));
        }


        [TestCase("", 0)]
        [TestCase("1",1)]
        [TestCase("1,2,3", 6)]
        [TestCase("1,2,,", 3)]
        public void Add_ValidInputStr_ValidResult(string str, int expectedResult)
        {

            //Act
            var actualResult = new Calculator().Add(str);

            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }


        [Test]
        public void Add_InvalidInputStr_ReturnNegativeException()
        {
            //Arrange
            string str = "1,-2,-3";
            string expectedExceptionMessage = "Không chấp nhận toán tử âm: -2, -3";

            //Act

            //Assert
            var exception = Assert.Throws<Exception>(() => new Calculator().Add(str));
            Assert.That(exception.Message, Is.EqualTo(expectedExceptionMessage));
        }

        [Test]
        public void Add_CantParseInt_ReturnNegativeException()
        {
            // Arrange
            string str = "1,a,2";
            string expectedExceptionMessage = "Không thể chuyển đổi a thành số nguyên.";

            // Act
            var exception = Assert.Throws<Exception>(() => new Calculator().Add(str));

            // Assert
            Assert.That(exception.Message, Is.EqualTo(expectedExceptionMessage));
        }


    }
}
