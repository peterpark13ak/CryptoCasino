using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.Utility.Wrappers
{
    public class DateWrapper : IDateWrapper
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}
