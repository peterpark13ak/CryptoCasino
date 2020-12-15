using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Web.Areas.Administration.Models;
using WebCasino.Web.Areas.Administration.Models.ChartModels;

namespace WebCasino.Web.Areas.Administration.Controllers
{
	[Area("Administration")]
	[Authorize(Roles = "Administrator")]
	public class HomeController : Controller
	{
		private readonly ITransactionService transactionService;
		private readonly IAdminDashboard adminDashboardService;
		private readonly IUserService userService;

		public HomeController(ITransactionService transactionService, IUserService userService, IAdminDashboard adminDashboardService)
		{
			this.transactionService = transactionService ?? throw new System.ArgumentNullException(nameof(transactionService));
			this.userService = userService ?? throw new System.ArgumentNullException(nameof(userService));
			this.adminDashboardService = adminDashboardService ?? throw new ArgumentNullException(nameof(adminDashboardService));
		}

		public async Task<IActionResult> Index()
		{
			var totalWins = await adminDashboardService.GetTotaTransactionsByTypeCount("Win");
			var totalStakes = await adminDashboardService.GetTotaTransactionsByTypeCount("Stake");

			var allUsers = await this.userService.GetAllUsers();
			var totalUsers = allUsers.Count();

			var allCurrencyDaylyWins = await this.adminDashboardService
				.GetTransactionsCurrencyDaylyWins(DateTime.Now.Day);

			var sixMonthsTotalWins = await this.adminDashboardService.GetMonthsTransactions(DateTime.Now, "Win", 5);
			var sixMonthsTotalStakes = await this.adminDashboardService.GetMonthsTransactions(DateTime.Now, "Stake", 5);

			var oneYearUsersCount = await this.userService.GetAllUsers();

			var oneYearWinsCount = await this.adminDashboardService.GetMonthsTransactions(DateTime.Now, "Win", 11);
			var oneYearStakeCount = await this.adminDashboardService.GetMonthsTransactions(DateTime.Now, "Stake", 11);

			var oneYearTransactions = await this.adminDashboardService.GetYearTransactions(DateTime.Now);

			var sixMonthsWins =  sixMonthsTotalWins.ValuesByMonth;
            var sixMonthsStakes =  sixMonthsTotalStakes.ValuesByMonth;

			var viewModel = new DashboardViewModel()
			{
				TotalWins = totalWins,
				TotalStakes = totalStakes,
				TotalUsers = totalUsers,

				SixMonthsTotalWins = sixMonthsWins.Where(v => v.Value > 0).Sum(x => x.Value),
				SixMonthsTotalStakes = sixMonthsStakes.Where(v => v.Value > 0).Sum(x => x.Value),
                SixMonthsWins = new MonthsModel(sixMonthsTotalWins.ValuesByMonth),
				SixMonthsStakes = new MonthsModel(sixMonthsTotalStakes.ValuesByMonth),

				OneYearTransactions = new MonthsModel(oneYearTransactions.ValuesByMonth),
				OneYearWins = new MonthsModel(oneYearWinsCount.ValuesByMonth),
				OneYearStakes = new MonthsModel(oneYearStakeCount.ValuesByMonth),

				DaylyWins = new DaylyWinsModel()
				{
					DaylyTotalUSD = allCurrencyDaylyWins.DaylyTotalUSD,
					DaylyWinsBGN = allCurrencyDaylyWins.DaylyWinsBGN,
					DaylyWinsEUR = allCurrencyDaylyWins.DaylyWinsEUR,
					DaylyWinsGBP = allCurrencyDaylyWins.DaylyWinsGBP,
					DaylyWinsUSD = allCurrencyDaylyWins.DaylyWinsUSD
				}
			};

			return this.View(viewModel);
		}
	}
}