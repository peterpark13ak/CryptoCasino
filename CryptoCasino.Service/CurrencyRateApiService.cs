using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Service.Utility.APICurrencyConvertor.Exceptions;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter.Models;
using WebCasino.Service.Utility.APICurrencyConvertor.RequestManager;
using WebCasino.Service.Utility.Wrappers;

namespace WebCasino.Service
{
    public class CurrencyRateApiService : ICurrencyRateApiService
    {

        private const string connection = "https://api.exchangeratesapi.io/latest?base=USD&symbols=EUR,BGN,GBP,USD";

        private ConcurrentDictionary<string, double> rates;

        private DateTime lastUpdate;

        private readonly IDateWrapper dateTime;

        private readonly IAPIRequester apiRequester;


        public CurrencyRateApiService(IAPIRequester apiRequester, IDateWrapper dateTime)
        {
            this.apiRequester = apiRequester;
            this.dateTime = dateTime;
        }

        public async Task<ConcurrentDictionary<string,double>> GetRatesAsync()
        {
            if(lastUpdate.AddHours(24) <= dateTime.Now() || rates == null)
            {
                var dic = this.RefreshRates();
                rates = new ConcurrentDictionary<string,double>(await this.RefreshRates());
            }
            return new ConcurrentDictionary<string,double>(rates);
        }

        private async Task<Dictionary<string,double>> RefreshRates()
        {
            var jsonResponse = await apiRequester.Request(connection);

            var model = JsonConvert.DeserializeObject<CurrencyRequestBindModel>(jsonResponse);

            if (model.Error == null)
            {
                lastUpdate = DateTime.UtcNow;
                return model.Rates; 
            }

            throw new ApiServiceNotFoundException(model.Error);
        }
    }
}
