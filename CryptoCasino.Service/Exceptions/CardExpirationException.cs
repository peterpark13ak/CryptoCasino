using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Exceptions
{
	public class CardExpirationException : Exception
	{
		public CardExpirationException(string message) : base(message)
		{
		}
	}
}
