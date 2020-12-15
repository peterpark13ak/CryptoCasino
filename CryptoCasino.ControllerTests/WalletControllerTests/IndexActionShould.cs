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
    public class IndexActionShould
    {
        [TestMethod]
        public async Task ReturnViewWithCorrectModel()
        {
            var userWrapper = SetupUserWrapper();

            var walletService = this.SetupWalletSerivce();

            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();

            var controller = this.ControllerSetup
                (walletService.Object, userWrapper.Object, cardService.Object, transService.Object);

            var result = await controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var view = (ViewResult)result;

            Assert.IsInstanceOfType(view.Model, typeof(WalletViewModel));

        }

        [TestMethod]
        public async Task CallUserWrapperOnce()
        {
            var cardService = new Mock<ICardService>();
            cardService.Setup(cs => cs.GetCard(It.IsAny<string>())).ReturnsAsync(new BankCard() { Id = "user-card" });

            var transService = new Mock<ITransactionService>();
            var userWrapper = SetupUserWrapper();

            var walletService = this.SetupWalletSerivce();

            var controller = 
                this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);

            await controller.Index();

            userWrapper.Verify(uw => uw.GetUserId(It.IsAny<ClaimsPrincipal>()), Times.Once);

        }

        [TestMethod]
        public async Task CallRetrieveWalletOnce()
        {
            var cardService = new Mock<ICardService>();
            var transService = new Mock<ITransactionService>();
            var userWrapper = SetupUserWrapper();

            var walletService = this.SetupWalletSerivce();

            var controller =
                this.ControllerSetup(walletService.Object, userWrapper.Object, cardService.Object, transService.Object);

            await controller.Index();

            walletService.Verify(ws => ws.RetrieveWallet(It.IsAny<string>()));
        }

        //Utility starts here
        private WalletController ControllerSetup
            (IWalletService walletService, IUserWrapper userWrapper, ICardService cardService, ITransactionService transactionService)
        {
            var controller = new WalletController
               (walletService, userWrapper, cardService, transactionService)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal()
                    }
                },
                TempData = new Mock<ITempDataDictionary>().Object
            };

            return controller;
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
                    Cards = new List<BankCard>(),
                    Transactions = new List<Transaction>()
                    
                }
            };

            var walletService = new Mock<IWalletService>();

            walletService.Setup(ws => ws.RetrieveWallet(It.IsAny<string>()))
                .ReturnsAsync(fakeWallet);

            return walletService;
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
