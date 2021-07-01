using NUnit.Framework;

namespace Console_Calculator.Test
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase("1-3*(-1/2+(3-2)*4/2)", -3.5)]
        [TestCase("1+3-4)", 0)]
        [TestCase("1-3", -2)]
        [TestCase("5+2", 7)]
        [TestCase("6+-6", 12)] //јлгоритм принимает только первый операнд
        [TestCase("6+(-6)", 0)] //ѕрибавить отрицательное число - только так (согласно правилам записи в математике)
        [TestCase("-1+3", 2)]
        [TestCase("1/2", 0.5)]
        [TestCase("-1/2+1/2", 0)]
        [TestCase("6/3*2", 4)]
        [TestCase("6/(-3)", -2)]
        [TestCase("-3/(-1)", 3)]
        [TestCase("-6-6", -12)]
        [TestCase("(-6)-(-6)", 0)]
        [TestCase("-1*(-1)", 1)]
        [TestCase("(-1)*(-1)", 1)]
        [TestCase("0/12+1", 1)]
        [TestCase("2+3*(-1+(-2*2)+1)", -10)]
        [TestCase("2,2+2.2", 4.4)]
        [TestCase("1 -   1", 0)]
        [TestCase("-(3+4)", -7)]
        public void CorrectExpression_Tests(string expression, decimal expected)
        {
            Calculator calc = new Calculator(expression);
            var actual = double.Parse(calc.Calculating());
            Assert.AreEqual(expected, actual);
        }
    }
}