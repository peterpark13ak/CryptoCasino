using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Exceptions
{
    public class InvalidAdministratorOperationException : Exception
    {
        public InvalidAdministratorOperationException(string message) : base(message)
        {

        }
    }
}
