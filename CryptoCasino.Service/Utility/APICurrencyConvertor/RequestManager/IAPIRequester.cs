using System.Threading.Tasks;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestManager
{
    public interface IAPIRequester
    {
        Task<string> Request(string connections);
    }
}