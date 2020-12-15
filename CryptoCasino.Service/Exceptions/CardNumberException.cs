using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Exceptions
{
	public class CardNumberException : Exception
	{
		public CardNumberException(string message) : base(message)
		{
		}
	}
}
