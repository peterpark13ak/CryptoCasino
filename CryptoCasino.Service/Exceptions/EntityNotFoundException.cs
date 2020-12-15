using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {

        }
    }
}
