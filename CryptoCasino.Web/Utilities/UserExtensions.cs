using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebCasino.Web.Utilities
{
    public static class UserExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier);

            return userId?.Value;
        }
    }
}
