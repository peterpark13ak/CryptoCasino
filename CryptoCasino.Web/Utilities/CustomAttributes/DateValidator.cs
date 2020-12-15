using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCasino.Web.Utilities.CustomAttributes
{
    public class DateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = Convert.ToDateTime(value);
            return date.AddYears(18) <= DateTime.Now.Date;
        }
    }
}
