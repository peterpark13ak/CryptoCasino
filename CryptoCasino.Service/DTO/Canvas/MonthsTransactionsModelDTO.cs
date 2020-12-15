using System.Collections.Generic;
using System.Linq;
using WebCasino.Entities;

namespace WebCasino.Service.DTO.Canvas
{
    public  class MonthsTransactionsModelDTO
    {
        public MonthsTransactionsModelDTO()
        {
            this.ValuesByMonth = new List<MonthVallueModelDTO>();
        }

        public IList<MonthVallueModelDTO> ValuesByMonth { get; set; }
    }
}
