using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCasino.Entities.Base;

namespace WebCasino.Entities
{
    public class CurrencyExchangeRate : Entity
    {
        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        [Range(0,double.MaxValue)]
        public double ExchangeRate { get; set; }
    }
}
