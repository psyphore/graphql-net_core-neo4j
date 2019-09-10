using BusinessServices;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServiceTests
{
    [TestFixture]
    public class Class1Tests
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
            var result = Class1.FindNumber(
                arr,
                k);

            // Assert
            Assert.AreEqual("YES", result);
        }

        [Test]
        public void OddNumbers_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            int l = 1;
            int r = 10;

            // Act
            var result = Class1.OddNumbers(
                l,
                r);

            // Assert
            Assert.Contains(2, result);
        }
    }
}