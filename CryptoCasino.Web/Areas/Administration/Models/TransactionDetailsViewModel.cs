using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
    public class TransactionDetailsViewModel
    {
        public TransactionDetailsViewModel()
        {

        }

        public TransactionDetailsViewModel(Transaction transaction)
        {
            this.Id = transaction.Id;
            this.CreatedOn = transaction.CreatedOn;
            this.User = transaction.User;
            this.TransactionType = transaction.TransactionType.Name;
            this.NormalisedAmount = transaction.NormalisedAmount;
            this.OriginalAmount = transaction.OriginalAmount;
            this.UserCurrency = transaction.User.Wallet.Currency.Name;
            this.Description = transaction.Description;
            
        }
        public string Id { get; set; }

        public User User { get; set; }

        public string UserCurrency { get; set; }

        public DateTime? CreatedOn { get; set; }

        public double NormalisedAmount { get; set; }
     
        public double OriginalAmount { get; set; }

        public string Description { get; set; }

        public string TransactionType { get; set; }
    }
}
