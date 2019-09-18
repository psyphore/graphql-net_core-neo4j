using BusinessServices;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
            var arr = Enumerable.Range(2,15).ToList();
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
    }
}