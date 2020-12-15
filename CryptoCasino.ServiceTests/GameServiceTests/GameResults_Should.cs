using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCasino.Service;
using WebCasino.Service.DTO.Game;
using WebCasino.Service.Utility.RandomGeneration;

namespace WebCasino.ServiceTests.GameServiceTests
{
    [TestClass]
    public class GameResults_Should
    {
        [TestMethod]
        [DataRow(5,0,100,1.6)]
        [DataRow(55, 0, 100, 2.4)]
        [DataRow(80, 0, 100, 3.2)]
        public void ReturnCorrectResultsWithoutWildSymbolsIO001(int num, int min, int max, double coef)
        {
            var rng = new Mock<RandomGenerator>();

            var gameService = new GameService(rng.Object);

            var gameBoard = new GameSymbolDTO[2, 2]
            {
                {new GameSymbolDTO(num,min,max), new GameSymbolDTO(num,min,max) },
                {new GameSymbolDTO(num,min,max), new GameSymbolDTO(num,min,max) }
            };

            var results = gameService.GameResults(gameBoard);

            Assert.AreEqual(coef, results.WinCoefficient);
        }


        [TestMethod]
        public void ReturnCorrectResultsWithoutWildSymbolsIO002()
        {
            var rng = new Mock<RandomGenerator>();

            var gameService = new GameService(rng.Object);

            var gameBoard = new GameSymbolDTO[2, 2]
            {
                {new GameSymbolDTO(80,0,100), new GameSymbolDTO(80,0,100) },
                {new GameSymbolDTO(30,0,100), new GameSymbolDTO(80,0,100) }
            };

            var results = gameService.GameResults(gameBoard);

            Assert.AreEqual(1.6, results.WinCoefficient);
        }

        [TestMethod]
        public void ReturnCorrectResultsWithoutWildSymbolsIO003()
        {
            var rng = new Mock<RandomGenerator>();

            var gameService = new GameService(rng.Object);

            var gameBoard = new GameSymbolDTO[2, 2]
            {
                {new GameSymbolDTO(30,0,100), new GameSymbolDTO(80,0,100) },
                {new GameSymbolDTO(80,0,100), new GameSymbolDTO(80,0,100) }
            };

            var results = gameService.GameResults(gameBoard);

            Assert.AreEqual(1.6, results.WinCoefficient);
        }

        [TestMethod]
        public void ReturnCorrectResultsWithoutWildSymbolsIO004()
        {
            var rng = new Mock<RandomGenerator>();

            var gameService = new GameService(rng.Object);

            var gameBoard = new GameSymbolDTO[2, 2]
            {
                {new GameSymbolDTO(30,0,100), new GameSymbolDTO(80,0,100) },
                {new GameSymbolDTO(30,0,100), new GameSymbolDTO(80,0,100) }
            };

            var results = gameService.GameResults(gameBoard);

            Assert.AreEqual(0, results.WinCoefficient);
        }

        [TestMethod]
        public void ReturnCorrectResultsWithWildIO001()
        {
            var rng = new Mock<RandomGenerator>();

            var gameService = new GameService(rng.Object);

            var gameBoard = new GameSymbolDTO[2, 2]
            {
                {new GameSymbolDTO(100,0,100), new GameSymbolDTO(80,0,100) },
                {new GameSymbolDTO(100,0,100), new GameSymbolDTO(80,0,100) }
            };

            var results = gameService.GameResults(gameBoard);

            Assert.AreEqual(1.6, results.WinCoefficient);
        }

        [TestMethod]
        public void ReturnCorrectResultsWithWildIO002()
        {
            var rng = new Mock<RandomGenerator>();

            var gameService = new GameService(rng.Object);

            var gameBoard = new GameSymbolDTO[2, 2]
            {
                {new GameSymbolDTO(80,0,100), new GameSymbolDTO(100,0,100) },
                {new GameSymbolDTO(80,0,100), new GameSymbolDTO(100,0,100) }
            };

            var results = gameService.GameResults(gameBoard);

            Assert.AreEqual(1.6, results.WinCoefficient);
        }

        [TestMethod]
        public void ReturnCorrectResultsWithWildIO003()
        {
            var rng = new Mock<RandomGenerator>();

            var gameService = new GameService(rng.Object);

            var gameBoard = new GameSymbolDTO[3, 3]
            {
                {new GameSymbolDTO(80,0,100), new GameSymbolDTO(100,0,100), new GameSymbolDTO(80,0,100) },
                {new GameSymbolDTO(80,0,100), new GameSymbolDTO(100,0,100), new GameSymbolDTO(80,0,100) },
                {new GameSymbolDTO(80,0,100), new GameSymbolDTO(100,0,100), new GameSymbolDTO(80,0,100) }
            };

            var results = gameService.GameResults(gameBoard);

            Assert.AreEqual(4.8, results.WinCoefficient);
        }
    }
}
