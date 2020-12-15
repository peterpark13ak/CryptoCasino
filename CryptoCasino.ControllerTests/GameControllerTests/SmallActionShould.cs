using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Web.Controllers;
using WebCasino.Web.Models.GameViewModels;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.ControllerTests.GameControllerTests
{
    [TestClass]
    public class SmallActionShould
    {
        [TestMethod]
        public void InvokeGenerateBoardMethod()
        {
            var gameService = new Mock<IGameService>();
            var controller = this.SetupController(gameService.Object);

            controller.Small();

            gameService.Verify(gs => gs.GenerateBoard(4, 3), Times.Once);

        }

        [TestMethod]
        public void ReturnCorrectActionResult()
        {
            var gameService = new Mock<IGameService>();
            var controller = this.SetupController(gameService.Object);

            var result = controller.Small();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var view = (ViewResult)result;

            Assert.IsInstanceOfType(view.Model, typeof(GameViewModel));
        }

        //Utility starts here
        private GameController SetupController(IGameService gameService)
        {
            var userWrapper = new Mock<IUserWrapper>();
            var transactionService = new Mock<ITransactionService>();

            return new GameController(gameService, transactionService.Object, userWrapper.Object);
        }
    }
}
