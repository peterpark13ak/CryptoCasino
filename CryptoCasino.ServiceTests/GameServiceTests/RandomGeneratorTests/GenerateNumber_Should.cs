using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCasino.Service.Utility.RandomGeneration;

namespace WebCasino.ServiceTests.GameServiceTests.RandomGeneratorTests
{
    [TestClass]
    public class GenerateNumber_Should
    {
        [TestMethod]
        public void ThrowIfInvalidParamsPassed()
        {
            var rng = new RandomGenerator();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => rng.GenerateNumber(5, 1, 5));
        }

        [TestMethod]
        public void ThrowIfAmountLessThanOne()
        {
            var rng = new RandomGenerator();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => rng.GenerateNumber(5, 10, -2));
        }

        [TestMethod]
        public void ReturnCollectionOfEqualElementsIfMinEqualsMax()
        {
            var rng = new RandomGenerator();
            var col = rng.GenerateNumber(5, 5, 5).ToList();
            Assert.AreEqual(5, col.Count);
            Assert.AreEqual(5, col.Max());
            Assert.AreEqual(5, col.Min());
        }

        [TestMethod]
        public void ReturnCollectionOfElementsWithinRange()
        {
            var rng = new RandomGenerator();
            var col = rng.GenerateNumber(0, 100, 10).ToList();

            Assert.AreEqual(10, col.Count);
            Assert.IsTrue(col.Max() <= 99);
            Assert.IsTrue(col.Min() >= 0);

        }
    }
}
