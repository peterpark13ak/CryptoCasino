using System.Collections.Generic;
using WebCasino.Service.DTO.Canvas;

namespace WebCasino.Web.Areas.Administration.Models.ChartModels
{
	public class MonthsModel
	{
		public Dictionary<string, int> monthValue = new Dictionary<string, int>();

		public MonthsModel(IList<MonthVallueModelDTO> sixMontsTransactions)
		{
			foreach (var item in sixMontsTransactions)
			{
				monthValue.Add(item.MonthValue, item.Value);
			}
		}
	}
}