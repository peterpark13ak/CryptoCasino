using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebCasino.Web.Utilities.Wrappers
{
    public class UserWrapper : IUserWrapper
    {
        public string GetUserId(ClaimsPrincipal user)
        {
            return user.GetId();
        }
    }
}
