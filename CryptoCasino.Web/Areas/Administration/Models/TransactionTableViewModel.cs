using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCasino.Web.Areas.Administration.Models
{
    public class TransactionTableViewModel
    {
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [JsonProperty(PropertyName = "recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty(PropertyName = "recordsFiltered")]
        public int RecordsFiltered { get; set; }
        [JsonProperty(PropertyName = "data")]
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}
