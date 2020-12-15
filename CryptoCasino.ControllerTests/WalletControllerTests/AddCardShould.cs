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
using WebCasino.Entities;
using WebCasino.Service.Abstract;
using WebCasino.Web.Controllers;
using WebCasino.Web.Models.WalletViewModels;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.ControllerTests.WalletControllerTests
{
    [TestClass]
    public class AddCardShould
    {
        [TestMethod]
        public async Task ReturnErrorTempDataIfInvalidModel()
        {
            var invalidModel = new CardViewModel();

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            controller.ModelState.AddModelError("error", "error");
            var result = await controller.AddCard(invalidModel);

            Assert.IsNotNull(controller.TempData["CardAddedFail"]);
            Assert.AreEqual("Failed to add card. Please try again with a different card", controller.TempData["CardAddedFail"]);


        }
        [TestMethod]
        public async Task RedirectIfInvalidModel()
        {
            var invalidModel = new CardViewModel();

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            controller.ModelState.AddModelError("error", "error");
            var result = await controller.AddCard(invalidModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectResult = (RedirectToActionResult)result;

            Assert.AreEqual("index", redirectResult.ActionName);
            Assert.AreEqual("wallet", redirectResult.ControllerName);
        }

        [TestMethod]
        public async Task SetTempDataErrorIfDateCannotBeParsed()
        {
            var invalidModel = new CardViewModel()
            {
                ExpirationDate = "12345"
            };

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            var result = await controller.AddCard(invalidModel);

            Assert.IsNotNull(controller.TempData["CardAddedFail"]);
            Assert.AreEqual("Invalid card date", controller.TempData["CardAddedFail"]);
        }


        [TestMethod]
        public async Task RedirectToCorrectActionIfDateCannotBeParsed()
        {
            var invalidModel = new CardViewModel()
            {
                ExpirationDate = "12345"
            };

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            var result = await controller.AddCard(invalidModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectResult = (RedirectToActionResult)result;

            Assert.AreEqual("index", redirectResult.ActionName);
            Assert.AreEqual("wallet", redirectResult.ControllerName);
        }

        [TestMethod]
        public async Task SetCorrectTempDataIfCardIsExpired()
        {
            var invalidModel = new CardViewModel()
            {
                ExpirationDate = "12/2012"
            };

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            var result = await controller.AddCard(invalidModel);

            Assert.IsNotNull(controller.TempData["CardAddedFail"]);
            Assert.AreEqual("Card is expired", controller.TempData["CardAddedFail"]);
        }

        [TestMethod]
        public async Task RedirectToCorrectActionIfDateIsExpired()
        {
            var invalidModel = new CardViewModel()
            {
                ExpirationDate = "12/2012"
            };

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            var result = await controller.AddCard(invalidModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectResult = (RedirectToActionResult)result;

            Assert.AreEqual("index", redirectResult.ActionName);
            Assert.AreEqual("wallet", redirectResult.ControllerName);
        }

        [TestMethod]
        public async Task CallCardServiceOnceIfValid()
        {
            var validModel = new CardViewModel()
            {
                ExpirationDate = "12/2019",
                RealNumber = "4444 4444 4444 4444"
            };

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            await controller.AddCard(validModel);


            Assert.IsNotNull(controller.TempData["CardAdded"]);
            Assert.AreEqual("Succesfulyy added new card", controller.TempData["CardAdded"]);
        }

        [TestMethod]
        public async Task CorrectlyRedirectIfEverythingIsValid()
        {
            var validModel = new CardViewModel()
            {
                ExpirationDate = "12/2019",
                RealNumber = "4444 4444 4444 4444"
            };

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            var result = await controller.AddCard(validModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectResult = (RedirectToActionResult)result;

            Assert.AreEqual("index", redirectResult.ActionName);
            Assert.AreEqual("wallet", redirectResult.ControllerName);
        }

        [TestMethod]
        public async Task SetCorrectTempDataIfEverythingValid()
        {
            var validModel = new CardViewModel()
            {
                ExpirationDate = "12/2019",
                RealNumber = "4444 4444 4444 4444"
            };

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            await controller.AddCard(validModel);

            cardService.Verify(cs => cs.AddCard(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }

        //Utility starts here
        private WalletController ControllerSetup
            (IWalletService walletService, IUserWrapper userWrapper, ICardService cardService, ITransactionService transactionService)
        {
            var defaultContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal()
            };
            var controller = new WalletController
               (walletService, userWrapper, cardService, transactionService)
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

    }
}
