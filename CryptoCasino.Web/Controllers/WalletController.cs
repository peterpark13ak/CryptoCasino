using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Service.Exceptions;
using WebCasino.Web.Models.WalletViewModels;
using WebCasino.Web.Utilities;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.Web.Controllers
{
    [Authorize(Roles = "Player")]
    public class WalletController : Controller
    {
        private readonly IWalletService walletService;
        private readonly IUserWrapper userWrapper;
        private readonly ICardService cardService;
        private readonly ITransactionService transactionService;

        public WalletController
            (IWalletService walletService, IUserWrapper userWrapper, ICardService cardService, ITransactionService transactionService)
        {
            this.walletService = walletService;
            this.userWrapper = userWrapper;
            this.cardService = cardService;
            this.transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.userWrapper.GetUserId(HttpContext.User);
            var wallet = await this.walletService.RetrieveWallet(userId);

            var model = new WalletViewModel(wallet);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCard(CardViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.userWrapper.GetUserId(HttpContext.User);

                DateTime date; 
                var validDate = DateTime.TryParseExact(model.ExpirationDate.Replace(" ", string.Empty), "MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
                if (!validDate)
                {
                    TempData["CardAddedFail"] = "Invalid card date";
                    return this.RedirectToAction("index", "wallet");
                }
                else if(date < DateTime.Now.Date)
                {
                    TempData["CardAddedFail"] = "Card is expired";
                    return this.RedirectToAction("index", "wallet");
                }
                else
                {
                    await this.cardService.AddCard(model.RealNumber.Replace(" ", string.Empty), userId, date);
                    TempData["CardAdded"] = "Succesfulyy added new card";
                    return this.RedirectToAction("index", "wallet");
                }

            }

            TempData["CardAddedFail"] = "Failed to add card. Please try again with a different card";
            return this.RedirectToAction("index", "wallet");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFunds(CardTransactionViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.userWrapper.GetUserId(HttpContext.User);
                var userWallet = await this.walletService.RetrieveWallet(userId);
                var userCurrency = ((CurrencyOptions)userWallet.CurrencyId).ToString();

                var card = await this.cardService.GetCard(model.CardId);
                if(card.UserId == userId)
                {
                    await this.transactionService.AddDepositTransaction
                        (userId, card.Id, model.Amount, $"Deposited {model.Amount} {userCurrency} with card ending in {card.CardNumber.Substring(12)}");
                    await this.cardService.Deposit(model.CardId, model.Amount);
                    TempData["SuccessfullDeposit"] = $"Succesfully deposited {model.Amount} {userCurrency}";
                    return this.RedirectToAction("index", "wallet");
                }
            }

            TempData["FailedDeposit"] = "Failed to deposit. Please try again with a different card";
            return this.RedirectToAction("index", "wallet");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WithdrawFunds(CardTransactionViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.userWrapper.GetUserId(HttpContext.User);
                var userWallet = await this.walletService.RetrieveWallet(userId);
                var userCurrency = ((CurrencyOptions)userWallet.CurrencyId).ToString();

                var card = await this.cardService.GetCard(model.CardId);
                if (card.UserId == userId)
                {
                    try
                    {
                        await this.transactionService.AddWithdrawTransaction
                       (userId, card.Id, model.Amount, $"Withdrew {model.Amount} {userCurrency} with card ending in {card.CardNumber.Substring(12)}");
                        await this.cardService.Withdraw(model.CardId, model.Amount);
                        TempData["SuccesfullWithdraw"] = $"Succesfully withdrew {model.Amount} {userCurrency}";
                        return this.RedirectToAction("index", "wallet");
                    }
                    catch(InsufficientFundsException exception)
                    {
                        TempData["FailedWithdraw"] = exception.Message;
                    }

                }
            }
            if(TempData["FailedWithdraw"] == null)
            {
                TempData["FailedWithdraw"] = "Failed to withdraw. Please try again with a different card";
            }          
            return this.RedirectToAction("index", "wallet");
        }
    }

}