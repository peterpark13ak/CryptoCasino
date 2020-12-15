using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCasino.Entities.Base;

namespace WebCasino.Entities
{
    public class BankCard : Entity
    {
        [Required]
        [RegularExpression("^[0-9]{16}$")]
        public string CardNumber { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Expiration { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Range(0,double.MaxValue)]
        public double MoneyAdded { get; set; }

        [Range(0, double.MaxValue)]
        public double MoneyRetrieved { get; set; }

        public IEnumerable<Transaction> Transcations { get; set; }

    }
}
