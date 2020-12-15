using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestManager
{
    public interface IHttpWrapper
    {
        Task<string> GetStringAsync(string connections);
    }
}
