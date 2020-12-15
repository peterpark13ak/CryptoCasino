using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;
using WebCasino.Web.Utilities.CustomAttributes;

namespace WebCasino.Web.Models.WalletViewModels
{
    public class CardViewModel
    {
        public CardViewModel()
        {

        }
        public CardViewModel(BankCard card)
        {
            CardId = card.Id;
            MaskedNumber = (card.CardNumber.Substring(12)).PadLeft(12,'*');
            MoneyAdded = card.MoneyAdded;
            MoneyRetrieved = card.MoneyRetrieved;

        }


        public string CardId { get; set; }

        [Required(ErrorMessage = "Please enter a valid card number")]
        [RegularExpression("^[0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4}$", ErrorMessage = "Please enter a valid card number")]
        public string RealNumber { get; set; }
        public string MaskedNumber { get; set; }

        [Required]
        public string ExpirationDate { get; set; }

        public double MoneyAdded { get; set; }

        public double MoneyRetrieved { get; set; }

    }
}
