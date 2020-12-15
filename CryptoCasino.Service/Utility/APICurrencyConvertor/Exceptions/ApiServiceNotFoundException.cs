using System;

namespace WebCasino.Service.Utility.APICurrencyConvertor.Exceptions
{
	public class ApiServiceNotFoundException : Exception
	{
		public ApiServiceNotFoundException(string message) : base(message)
		{
		}
	}
}