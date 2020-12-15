using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Exceptions
{
    public class NotValidDayInMonthException : Exception
    {
        public NotValidDayInMonthException(string message) : base(message)
        {

        }
    }
}
