using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCasino.Service.Abstract
{
    public interface ICurrencyRateApiService
    {
        Task<ConcurrentDictionary<string, double>> GetRatesAsync();
    }
}