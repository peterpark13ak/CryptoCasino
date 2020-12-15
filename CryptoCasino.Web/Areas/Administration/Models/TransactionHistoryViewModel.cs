using System.Collections.Generic;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class TransactionHistoryViewModel
	{
      
		public IEnumerable<TransactionViewModel> Transactions { get; set; }



	}
}