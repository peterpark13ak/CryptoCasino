using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Web.Models.ViewComponentModels.UserNormalizedBalanceModels;
using WebCasino.Web.Utilities;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.Web.ViewComponents
{
    public class UserNormalizedBalanceWarningViewComponent : ViewComponent
    {
        private readonly IUserWrapper userManager;
        private readonly IUserService userService;
        private readonly ICurrencyRateApiService ratesService;

        public UserNormalizedBalanceWarningViewComponent(IUserWrapper userManager, IUserService userService, ICurrencyRateApiService ratesService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.ratesService = ratesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = userManager.GetUserId(HttpContext.User);

            var user = await userService.RetrieveUser(userId);

            var normalizedBalance = user.Wallet.NormalisedBalance;

            var userCurrency = ((CurrencyOptions)user.Wallet.CurrencyId).ToString();

            var exchangeRates = await ratesService.GetRatesAsync();

            if (exchangeRates.ContainsKey(userCurrency))
            {
                var maximumWithdraw = normalizedBalance * exchangeRates[userCurrency];
                return View(new NormalizedBalanceViewModel(userCurrency,maximumWithdraw));
            }

            return View("Error");
        }
    }
}
