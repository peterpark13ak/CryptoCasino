using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Web.Utilities.Middlewares;

namespace WebCasino.Web.Utilities
{
    public static class AppBuilderExtensions
    {
        public static void ImportantExceptionHandling(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
