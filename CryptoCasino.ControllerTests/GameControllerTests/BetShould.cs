using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Service.DTO.Game;
using WebCasino.Web.Controllers;
using WebCasino.Web.Models.GameViewModels;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.ControllerTests.GameControllerTests
{
    [TestClass]
    public class BetShould
    {
        private readonly GameSymbolDTO[,] gameBoard = new GameSymbolDTO[2, 2]
                {
                    { new GameSymbolDTO(13,0,100),new GameSymbolDTO(13,0,100) },
                    { new GameSymbolDTO(13,0,100),new GameSymbolDTO(13,0,100) }
                };

        [TestMethod]
        public async Task SetCorrectTempDataIfInvalidModel()
        {
            var gameSerivce = new Mock<IGameService>();
            var userWrapper = this.SetupUserWrapper();
            var transactionService = new Mock<ITransactionService>();

            var controller = this.ControllerSetup(gameSerivce.Object, transactionService.Object, userWrapper.Object);
            controller.ModelState.AddModelError("error", "error");

            var model = new GameViewModel();

            await controller.Bet(model);

            Assert.IsNotNull(controller.TempData["Invalid bet"]);
            Assert.AreEqual("We couldn't place your bet", controller.TempData["Invalid bet"]);

        }

        [TestMethod]
        public async Task ReturnCorrectPartialIfInvalidModel()
        {
            var gameSerivce = new Mock<IGameService>();
            var userWrapper = this.SetupUserWrapper();
            var transactionService = new Mock<ITransactionService>();

            var controller = this.ControllerSetup(gameSerivce.Object, transactionService.Object, userWrapper.Object);
            controller.ModelState.AddModelError("error", "error");

            var model = new GameViewModel();

            var result = await controller.Bet(model);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

        }

        [TestMethod]
        public async Task InvokeUserWrapperMethodOnceIfValidModel()
        {
            var gameSerivce = this.SetupGameService(true);
            var userWrapper = this.SetupUserWrapper();
            var transactionService = new Mock<ITransactionService>();

            var controller = this.ControllerSetup(gameSerivce.Object, transactionService.Object, userWrapper.Object);

            var model = this.SetupValidModel();

            await controller.Bet(model);

            userWrapper.Verify(uw => uw.GetUserId(It.IsAny<ClaimsPrincipal>()), Times.Once);
        }

        [TestMethod]
        public async Task InvokeGenerateBoardMethodOnceIfValidModel()
        {
            var gameSerivce = this.SetupGameService(true);
            var userWrapper = this.SetupUserWrapper();
            var transactionService = new Mock<ITransactionService>();

            var controller = this.ControllerSetup(gameSerivce.Object, transactionService.Object, userWrapper.Object);

            var model = this.SetupValidModel();

            await controller.Bet(model);

            gameSerivce.Verify(uw => uw.GenerateBoard(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task InvokeGameResultsMethodOnceIfValidModel()
        {
            var gameSerivce = this.SetupGameService(true);
            var userWrapper = this.SetupUserWrapper();
            var transactionService = new Mock<ITransactionService>();

            var controller = this.ControllerSetup(gameSerivce.Object, transactionService.Object, userWrapper.Object);

            var model = this.SetupValidModel();

            await controller.Bet(model);

            gameSerivce.Verify(uw => uw.GameResults(this.gameBoard), Times.Once);
        }

        [TestMethod]
        public async Task SkipTransactionProcessIfBetLost()
        {
            var gameSerivce = this.SetupGameService(false);
            var userWrapper = this.SetupUserWrapper();
            var transactionService = new Mock<ITransactionService>();

            var controller = this.ControllerSetup(gameSerivce.Object, transactionService.Object, userWrapper.Object);

            var model = this.SetupValidModel();

            await controller.Bet(model);

            transactionService.Verify(ts 
                => ts.AddWinTransaction(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task MakeTransactionIfBetWon()
        {
            var gameSerivce = this.SetupGameService(true);
            var userWrapper = this.SetupUserWrapper();
            var transactionService = new Mock<ITransactionService>();

            var controller = this.ControllerSetup(gameSerivce.Object, transactionService.Object, userWrapper.Object);

            var model = this.SetupValidModel();

            await controller.Bet(model);

            transactionService.Verify(ts
                => ts.AddWinTransaction(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task ReturnCorrectActionResultIfModelValid()
        {
            var gameSerivce = this.SetupGameService(true);
            var userWrapper = this.SetupUserWrapper();
            var transactionService = new Mock<ITransactionService>();

            var controller = this.ControllerSetup(gameSerivce.Object, transactionService.Object, userWrapper.Object);

            var model = this.SetupValidModel();

            var result = await controller.Bet(model);

            Assert.IsInstanceOfType(result, typeof(JsonResult));

        }
        //Utility starts here

        private GameController ControllerSetup
       (IGameService gameService, ITransactionService transactionService, IUserWrapper userWrapper)
        {
            var defaultContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal()
            };
            var controller = new GameController
               (gameService, transactionService, userWrapper)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = defaultContext
                },
                TempData = new TempDataDictionary(defaultContext, new Mock<ITempDataProvider>().Object)
            };

            return controller;
        }
        private Mock<IUserWrapper> SetupUserWrapper()
        {
            var userWrapper = new Mock<IUserWrapper>();
            userWrapper.Setup(uw => uw.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("mocked-user");

            return userWrapper;
        }

        private GameViewModel SetupValidModel()
        {
            var model = new GameViewModel()
            {
                BetAmount = 5,
                Board = this.gameBoard
            };

            return model;
        }

        private Mock<IGameService> SetupGameService(bool isWinning)
        {
            var gameService = new Mock<IGameService>();
            gameService.Setup(gs => gs.GenerateBoard(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(gameBoard);

            if (isWinning)
            {
                gameService.Setup(gs => gs.GameResults(It.IsAny<GameSymbolDTO[,]>()))
                    .Returns(new GameResultsDTO()
                    {
                        WinCoefficient = 3,
                        GameBoard = this.gameBoard
                    });
            }

            else
            {
                gameService.Setup(gs => gs.GameResults(It.IsAny<GameSymbolDTO[,]>()))
                    .Returns(new GameResultsDTO()
                    {
                        WinCoefficient = 0,
                        GameBoard = this.gameBoard
                            
                    });
            }
            return gameService;
        }
    }
}
