using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCasino.Web.Models.ViewComponentModels.UserNormalizedBalanceModels
{
    public class NormalizedBalanceViewModel
    {
        public NormalizedBalanceViewModel(string currency, double amount)
        {
            Currency = currency;
            Amount = Math.Floor(amount*100)/100;
        }

        public string Currency { get; set; }

        public double Amount { get; set; }
    }
}
