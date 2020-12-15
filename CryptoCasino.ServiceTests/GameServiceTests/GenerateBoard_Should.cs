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
    public class GenerateBoard_Should
    {
        [TestMethod]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        public void ThrowIfInvalidParametersPassed(int rows, int cols)
        {
            var mockedGenerator = new Mock<IRandomGenerator>();

            var gameService = new GameService(mockedGenerator.Object);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => gameService.GenerateBoard(rows, cols));
        }

        [TestMethod]
        public void ReturnMatrixOfCorrectSizeAndCorrectType()
        {
            var mockedGenerator = new Mock<IRandomGenerator>();

            mockedGenerator.Setup(rng => rng.GenerateNumber(0, 101, 25))
                .Returns(Enumerable.Repeat(5, 25).ToList());

            var gameService = new GameService(mockedGenerator.Object);

            var board = gameService.GenerateBoard(5, 5);

            Assert.IsTrue(board.GetLength(0) == 5);
            Assert.IsTrue(board.GetLength(1) == 5);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int f = 0; f < board.GetLength(1); f++)
                {
                    Assert.IsNotNull(board[i, f]);
                    Assert.IsTrue(board[i, f].GetType() == typeof(GameSymbolDTO));
                }
            }

        }
    }
}
