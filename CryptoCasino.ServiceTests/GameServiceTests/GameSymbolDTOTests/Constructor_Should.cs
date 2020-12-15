using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebCasino.Service.DTO.Game;

namespace WebCasino.ServiceTests.GameServiceTests.GameSymbolDTOTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        [DataRow(101, 0, 100)]
        [DataRow(-1, 0, 100)]
        public void ThrowWhenInvalidParametersPassed(int number, int min, int max)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new GameSymbolDTO(number, min, max));
        }

        [TestMethod]
        [DataRow(1, 0, 100, 0.4, "low")]
        [DataRow(5,0,100,0.4,"low")]
        [DataRow(45, 0, 100, 0.6, "medium")]
        [DataRow(79, 0, 100, 0.6, "medium")]
        [DataRow(81, 0, 100, 0.8, "high")]
        [DataRow(94, 0, 100, 0.8, "high")]
        [DataRow(95, 0, 100, 0, "wild")]
        [DataRow(99, 0, 100, 0, "wild")]
        public void PopulatePropertiesWhenValidParameters(int num, int min, int max, double coef, string name)
        {
            var gameSymbol = new GameSymbolDTO(num,min,max);
            Assert.AreEqual(coef, gameSymbol.Coefficient);
            Assert.AreEqual(name, gameSymbol.SymbolName);
        }
    }


}
