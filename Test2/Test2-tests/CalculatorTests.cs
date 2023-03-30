using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test2;

namespace Test2_tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var input = string.Empty;
            int expectedAnswer = 0;

            //Act 
            var result = StringCalculator.Add(input);

            //Assert
            Assert.AreEqual(expectedAnswer, result, 0, "Result not calculated correctly");
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            var input = "1,2";
            int expectedAnswer = 3;

            //Act 
            var result = StringCalculator.Add(input);

            //Assert
            Assert.AreEqual(expectedAnswer, result, 0, "Result not calculated correctly");
        }

        [TestMethod]
        public void TestMethod3()
        {
            // Arrange
            var input = "1\n2,3";
            int expectedAnswer = 6;

            //Act 
            var result = StringCalculator.Add(input);

            //Assert
            Assert.AreEqual(expectedAnswer, result, 0, "Result not calculated correctly");
        }

        [TestMethod]
        public void TestMethod4()
        {
            // Arrange
            var input = "//|\n6|8";
            int expectedAnswer = 14;

            //Act 
            var result = StringCalculator.Add(input);

            //Assert
            Assert.AreEqual(expectedAnswer, result, 0, "Result not calculated correctly");
        }


        [TestMethod]
        public void TestMethod5()
        {
            // Arrange
            var input = "//|\n6|8|9";
            int expectedAnswer = 23;

            //Act 
            var result = StringCalculator.Add(input);

            //Assert
            Assert.AreEqual(expectedAnswer, result, 0, "Result not calculated correctly");
        }

        // Running on a various combination of delimiters
        [TestMethod]
        public void TestMethod6()
        {
            // Arrange
            var input = "//|\n6|8,9\n5";
            int expectedAnswer = 28;

            //Act 
            var result = StringCalculator.Add(input);

            //Assert
            Assert.AreEqual(expectedAnswer, result, 0, "Result not calculated correctly");
        }


        // Running on a long list of numbers
        [TestMethod]
        public void TestMethod7()
        {
            // Arrange
            var input = "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
            int expectedAnswer = 36;

            //Act 
            var result = StringCalculator.Add(input);

            //Assert
            Assert.AreEqual(expectedAnswer, result, 0, "Result not calculated correctly");
        }
    }
}
