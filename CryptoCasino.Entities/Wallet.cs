using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCasino.Entities.Base;

namespace WebCasino.Entities
{
    public class Wallet : Entity
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        [Range(0, double.MaxValue)]
        public double DisplayBalance { get; set; }

        [Range(0, double.MaxValue)]
        public double NormalisedBalance { get; set; }

        [Range(0, double.MaxValue)]
        public double Wins { get; set; }

        [Range(0, double.MaxValue)]
        public double Stakes { get; set; }

        [Range(0, double.MaxValue)]
        public double Deposits { get; set; }
    }
}
