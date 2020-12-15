using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


using WebCasino.Service.Utility.Validator;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestManager
{
	public class APIRequester : IAPIRequester
    {
		private readonly IHttpWrapper client;
		
		public APIRequester(IHttpWrapper client)
		{
			this.client = client ?? throw new ArgumentNullException(nameof(client));			
		}

		public async Task<string> Request(string connections)
		{
			ServiceValidator.IsInputStringEmptyOrNull(connections);

			var response = await client.GetStringAsync(connections);

			ServiceValidator.IsInputStringEmptyOrNull(response);

			return response;
		}
	}
}