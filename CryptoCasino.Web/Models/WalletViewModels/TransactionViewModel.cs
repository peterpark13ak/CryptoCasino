using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Web.Models.WalletViewModels
{
    public class TransactionViewModel
    {
        public TransactionViewModel(Transaction transaction)
        {
            Amount = transaction.OriginalAmount;
            TransactionTypeName = transaction.TransactionType.Name;
            CreatedOn = ((DateTime)transaction.CreatedOn).ToShortDateString();
            Description = transaction.Description;
        }

        public string CreatedOn { get; set; }
        public double Amount { get; set; }

        public string TransactionTypeName { get; set; }

        public string Description { get; set; }
    }
}
