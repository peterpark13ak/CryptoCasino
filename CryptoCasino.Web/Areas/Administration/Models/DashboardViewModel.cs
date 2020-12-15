using WebCasino.Web.Areas.Administration.Models.ChartModels;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class DashboardViewModel
	{
		public DashboardViewModel()
		{
		}

		public int TotalWins { get; set; }
		public int TotalStakes { get; set; }
		public int TotalUsers { get; set; }

		public int SixMonthsTotalWins { get; set; }
		public MonthsModel SixMonthsWins { get; set; }

		public int SixMonthsTotalStakes { get; set; }
		public MonthsModel SixMonthsStakes { get; set; }

		//TODO : CREATE NEW MODEL OR RENAME SixMonthsModel
		public MonthsModel OneYearWins { get; set; }

		public MonthsModel OneYearStakes { get; set; }
		public MonthsModel OneYearTransactions { get; set; }

		public DaylyWinsModel DaylyWins { get; set; }
	}
}