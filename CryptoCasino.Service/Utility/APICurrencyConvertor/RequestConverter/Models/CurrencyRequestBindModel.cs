using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebCasino.Service.Utility.APICurrencyConvertor.RequestConverter.Models
{
	public class CurrencyRequestBindModel
	{
		[JsonProperty("base")]
		public string Base { get; set; }

		[JsonProperty("date")]
		public string Date { get; set; }

		[JsonProperty("rates")]
		public Dictionary<string, double> Rates { get; set; }

		[JsonProperty("error")]
		public string Error { get; set; }
	}
}