using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestManager
{
    public class HttpWrapper : IHttpWrapper
    {
        private readonly HttpClient client;

        public HttpWrapper(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> GetStringAsync(string connections)
        {
            return await client.GetStringAsync(connections);
        }
    }
}
