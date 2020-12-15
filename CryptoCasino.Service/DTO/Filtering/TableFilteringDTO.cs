using System;
using System.Collections.Generic;
using System.Text;
using WebCasino.Entities;

namespace WebCasino.Service.DTO.Filtering
{
    public class TableFilteringDTO
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }

    }
}
