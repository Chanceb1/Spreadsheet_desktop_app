namespace SpreadsheetTests
{
    using System.Globalization;
    using System.Reflection;
    using SpreadsheetEngine;

    public class ExpressionTreeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestExpressionTreeCreation()
        {
            ExpressionTree expressionTree = new ExpressionTree("3+5");
            Assert.That(expressionTree, Is.Not.Null);
        }

        [Test]
        [TestCase("3+5", ExpectedResult = 8.0)]
        [TestCase("100/10*10", ExpectedResult = 100.0)]
        [TestCase("100/(10*10)", ExpectedResult = 1.0)]
        [TestCase("7-4+2", ExpectedResult = 5.0)]
        [TestCase("10/(7-2)", ExpectedResult = 2.0)]
        [TestCase("(12-2)/2", ExpectedResult = 5.0)]
        [TestCase("(((((2+3)-(4+5)))))", ExpectedResult = -4.0)]
        [TestCase("2*3+5", ExpectedResult = 11.0)]
        [TestCase("2+3*5", ExpectedResult = 17.0)]
        [TestCase("5/0", ExpectedResult = double.PositiveInfinity)]
        public double TestExpressionEvaluation(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        [Test]
        public void TestExpressionWithVariables()
        {
            ExpressionTree exp = new ExpressionTree("A3+5");
            exp.SetVariable("A3", 23);
            Assert.That(exp.Evaluate(), Is.EqualTo(28));

            exp = new ExpressionTree("B2+A3*5");
            exp.SetVariable("A3", 3);
            exp.SetVariable("B2", 2);
            Assert.That(exp.Evaluate(), Is.EqualTo(17));
        }
    }
}