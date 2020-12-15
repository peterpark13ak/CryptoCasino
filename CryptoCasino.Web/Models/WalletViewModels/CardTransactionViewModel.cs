using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Entities;

namespace WebCasino.Web.Models.WalletViewModels
{
    public class CardTransactionViewModel
    {
        public CardTransactionViewModel()
        {

        }
        public CardTransactionViewModel(IEnumerable<CardViewModel> cards)
        {
            CardSelect = cards.Select(card => new SelectListItem() { Text = card.MaskedNumber, Value = card.CardId });
        }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a valid amount")]
        public double Amount { get; set; }

        [Required]
        public string CardId { get; set; }

        [Required(ErrorMessage = "Please enter a valid CVV")]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "Please enter a valid CVV")]
        public string Cvv { get; set; }

        public IEnumerable<SelectListItem> CardSelect { get; set; }

    }
}
