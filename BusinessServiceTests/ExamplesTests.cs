using BusinessServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessServiceTests
{
    [TestFixture]
    public class ExamplesTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void FindNumber_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var arr = Enumerable.Range(2, 15).ToList();
            int k = 3;

            // Act
            var result = Examples.FindNumber(
                arr,
                k);

            // Assert
            Assert.AreEqual("YES", result);
        }

        [Test]
        public void OddNumbers_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            int l = 2;
            int r = 5;

            // Act
            var result = Examples.OddNumbers(
                l,
                r);

            // Assert
            Assert.Contains(3, result);
            Assert.Contains(5, result);
        }

        [Test]
        public void example()
        {
            // Arrange & Act
            bool x = true || false;

            // Assert
            Assert.IsTrue(x);
        }

        [Test]
        public void example18()
        {
            // Arrange
            string value1 = "1", value2 = "0", action = "Divide", msg = "", expected1 = "";
            decimal expected2 = -9999M;

            // Act
            var result = Examples.Calc(value1, value2, action, out msg);

            // Assert
            Assert.AreNotEqual(msg, expected1);
            Assert.AreEqual(expected2, result);
        }

        [Test]
        public void example22()
        {
            // Arrange
            DateTime input1 = new DateTime(1990, 12, 2);
            int input2 = 148;
            string expected = "00148-901202";

            // Act
            var result = Examples.FormatAsCustomString(input1, input2);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void example8()
        {
            // Arrange
            var range = new List<int>(Enumerable.Range(1, 10));

            // Act
            for (int i = 9; i >= 0; i--)
                range.RemoveAt(i);

            // Assert
            Assert.IsEmpty(range);
        }

        [Test]
        public void example11()
        {
            // Arrange
            var value = "(+27) 82-652-5623, (+1) 776-956-1201";

            // Act
            var rs = new[]
            {
                Regex.Match(value, @"^\(\+(\d\d?\d?)\) (\d\d?\d?)-(\d{3})-(\d{4})")
            };

            // Assert
            Assert.IsTrue(rs.First().Success);
        }

        [Test]
        public void example16()
        {
            // Arrange
            var dp = new DataProcessor();
            var expected = new[] { 0, 2, 6, 15, 32, 65, 126, 238, 440, 801 };

            // Act
            dp.InititialiseData(new[] { 10, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 });
            dp.CalculateData();
            var result = dp.GetResults().ToArray();

            // Assert
            Assert.AreEqual(expected[2], result[2]);
        }

        [Test]
        public void example17()
        {
            // Arrange
            int value = 3, previousValue = 0, currentValue = 1;

            // Act
            var result = Examples.IsFibo(value, previousValue, currentValue);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void example21()
        {
            // Arrange
            var value = new[] { "Sarah", "Mike", "John", "Mike" };
            var expected = new Dictionary<string, int>
            {
                { "Sarah", 1 },
                { "Mike", 2 },
                { "John", 1 }
            };

            // Act
            var result = Examples.AggregateNames(value);

            // Assert
            Assert.AreEqual(expected["Mike"], result["Mike"]);
        }

        [Test]
        public void example23()
        {
            // Arrange
            var value = "A helicopter is a type of rotorcraft.";
            var expected = "A retpocileh si a epyt fo tfarcrotor.";

            // Act
            var result = Examples.ReverseWords(value);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}