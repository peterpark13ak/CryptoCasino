using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCasino.Entities.Base;

namespace WebCasino.Entities
{
    public class Transaction : Entity
    {
        public User User { get; set; }
        public string UserId { get; set; }

        [Range(0, double.MaxValue)]
        public double NormalisedAmount { get; set; }

        [Range(0, double.MaxValue)]
        public double OriginalAmount { get; set; }

        public BankCard Card { get; set; }

        public string CardId { get; set; }

        [Range(10,100)]
        public string Description { get; set; }

        [Required]
        public int TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
