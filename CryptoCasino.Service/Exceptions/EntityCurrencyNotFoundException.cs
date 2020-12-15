using System;

namespace WebCasino.Service.Exceptions
{
	public class EntityCurrencyNotFoundException : Exception
	{
		public EntityCurrencyNotFoundException(string message) : base(message)
		{
		}
	}
}