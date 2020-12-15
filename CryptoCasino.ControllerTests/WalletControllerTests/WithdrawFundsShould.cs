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
using WebCasino.Service.Exceptions;
using WebCasino.Web.Controllers;
using WebCasino.Web.Models.WalletViewModels;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.ControllerTests.WalletControllerTests
{
    [TestClass]
    public class WithdrawFundsShould
    {
        private const string userId = "mocked-user";
        private const string cardId = "bank-card";
        private const double amount = 50;

        [TestMethod]
        public async Task ReturnErrorTempDataIfInvalidModel()
        {
            var invalidModel = new CardTransactionViewModel();

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            controller.ModelState.AddModelError("error", "error");
            await controller.WithdrawFunds(invalidModel);

            Assert.IsNotNull(controller.TempData["FailedWithdraw"]);
            Assert.AreEqual("Failed to withdraw. Please try again with a different card", controller.TempData["FailedWithdraw"]);
        }

        [TestMethod]
        public async Task RedirectCorrectlyIfInvalidModel()
        {
            var invalidModel = new CardTransactionViewModel();

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = new Mock<IWalletService>();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            controller.ModelState.AddModelError("error", "error");
            var result = await controller.WithdrawFunds(invalidModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectResult = (RedirectToActionResult)result;

            Assert.AreEqual("index", redirectResult.ActionName);
            Assert.AreEqual("wallet", redirectResult.ControllerName);
        }

        [TestMethod]
        public async Task ReturnErrorTempDataIfCardNotBelongingToUser()
        {
            var invalidModel = new CardTransactionViewModel();

            var cardService = this.SetupCardService();
            var transService = new Mock<ITransactionService>();
            var userWrapper = new Mock<IUserWrapper>();
            var walletService = this.SetupWalletSerivce();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            await controller.WithdrawFunds(invalidModel);

            Assert.IsNotNull(controller.TempData["FailedWithdraw"]);
            Assert.AreEqual("Failed to withdraw. Please try again with a different card", controller.TempData["FailedWithdraw"]);
        }

        [TestMethod]
        public async Task RedirectCorrectlyIfCardNotBelongingToUser()
        {
            var invalidModel = new CardTransactionViewModel();

            var cardService = this.SetupCardService();
            var transService = new Mock<ITransactionService>();
            var userWrapper = new Mock<IUserWrapper>();
            var walletService = this.SetupWalletSerivce();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            var result = await controller.WithdrawFunds(invalidModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectResult = (RedirectToActionResult)result;

            Assert.AreEqual("index", redirectResult.ActionName);
            Assert.AreEqual("wallet", redirectResult.ControllerName);
        }

        [TestMethod]
        public async Task CallsGetCardMethodIfValidModel()
        {
            var validModel = this.SetupValidModel();

            var cardService = SetupCardService();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = SetupWalletSerivce();


            var controller = this.ControllerSetup
                (walletService.Object, userWrapper.Object, cardService.Object, transService.Object);

            await controller.WithdrawFunds(validModel);

            cardService.Verify(cs => cs.GetCard(WithdrawFundsShould.cardId), Times.Once);
        }

        [TestMethod]
        public async Task CallsAddWithdrawTransactionIfValidModel()
        {
            var validModel = this.SetupValidModel();
            var cardService = SetupCardService();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = SetupWalletSerivce();


            var controller = this.ControllerSetup
                (walletService.Object, userWrapper.Object, cardService.Object, transService.Object);

            await controller.WithdrawFunds(validModel);
            transService.Verify(ts
                => ts.AddWithdrawTransaction(WithdrawFundsShould.userId, It.IsAny<string>(), WithdrawFundsShould.amount, It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task CallsCardServiceWithdrawIfValidModel()
        {
            var validModel = this.SetupValidModel();
            var cardService = SetupCardService();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = SetupWalletSerivce();


            var controller = this.ControllerSetup
                (walletService.Object, userWrapper.Object, cardService.Object, transService.Object);

            await controller.WithdrawFunds(validModel);
            cardService.Verify(cs
                => cs.Withdraw(WithdrawFundsShould.cardId, validModel.Amount), Times.Once);
        }

        [TestMethod]
        public async Task SetCorrectTempDataIfValidModel()
        {
            var validModel = this.SetupValidModel();
            var cardService = SetupCardService();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = SetupWalletSerivce();


            var controller = this.ControllerSetup
                (walletService.Object, userWrapper.Object, cardService.Object, transService.Object);

            await controller.WithdrawFunds(validModel);

            Assert.IsNotNull(controller.TempData["SuccesfullWithdraw"]);
            Assert.IsTrue(controller.TempData["SuccesfullWithdraw"].ToString().Contains("Succesfully withdrew"));
        }

        [TestMethod]
        public async Task RedirectToCorrectlyIfValidModel()
        {
            var invalidModel = new CardTransactionViewModel();

            var validModel = this.SetupValidModel();
            var cardService = SetupCardService();
            var transService = new Mock<ITransactionService>();
            var userWrapper = this.SetupUserWrapper();
            var walletService = SetupWalletSerivce();


            var controller = this.ControllerSetup
                (walletService.Object, userWrapper.Object, cardService.Object, transService.Object);

            var result = await controller.WithdrawFunds(validModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectResult = (RedirectToActionResult)result;

            Assert.AreEqual("index", redirectResult.ActionName);
            Assert.AreEqual("wallet", redirectResult.ControllerName);
        }

        [TestMethod]
        public async Task SetCorrectTempDataIfInsufficientFundsExceptionCaught()
        {
            var invalidModel = new CardTransactionViewModel();

            var cardService = this.SetupCardService();
            var transService = new Mock<ITransactionService>();

            transService.Setup(ts
                => ts.AddWithdrawTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>()))
                .Throws(new InsufficientFundsException("Insufficient funds"));

            var userWrapper = this.SetupUserWrapper();
            var walletService = this.SetupWalletSerivce();

            var controller = this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);
            await controller.WithdrawFunds(invalidModel);

            Assert.IsNotNull(controller.TempData["FailedWithdraw"]);
            Assert.AreEqual("Insufficient funds", controller.TempData["FailedWithdraw"]);
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
                .Returns(WithdrawFundsShould.userId);

            return userWrapper;
        }


        private Mock<IWalletService> SetupWalletSerivce()
        {
            var fakeWallet = new Wallet()
            {
                CurrencyId = 1,
                Wins = 1,
                DisplayBalance = 50,
                NormalisedBalance = 50,
                User = new User()
                {
                    Cards = new List<BankCard>()
                }
            };

            var walletService = new Mock<IWalletService>();

            walletService.Setup(ws => ws.RetrieveWallet(It.IsAny<string>()))
                .ReturnsAsync(fakeWallet);

            return walletService;
        }

        private Mock<ICardService> SetupCardService()
        {
            var cardService = new Mock<ICardService>();

            cardService.Setup(cs => cs.GetCard(It.IsAny<string>()))
                .ReturnsAsync(new BankCard() { UserId = "mocked-user", CardNumber = "4444444444444444" });

            return cardService;
        }

        private CardTransactionViewModel SetupValidModel()
        {
            return new CardTransactionViewModel()
            {
                CardId = WithdrawFundsShould.cardId,
                Amount = WithdrawFundsShould.amount
            };
        }
    }
}

