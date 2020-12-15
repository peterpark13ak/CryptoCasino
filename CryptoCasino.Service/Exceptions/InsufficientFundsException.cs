using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message)
        {

        }
    }
}
