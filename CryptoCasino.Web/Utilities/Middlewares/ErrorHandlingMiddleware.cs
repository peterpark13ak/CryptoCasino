using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Service.Exceptions;

namespace WebCasino.Web.Utilities.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (EntityNotFoundException)
            {
                context.Response.Redirect("/home/invalid");
            }
            catch (EntityCurrencyNotFoundException)
            {
                context.Response.Redirect("/home/invalid");
            }
            catch (Exception)
            {
                context.Response.Redirect("/home/servererror");
            }
        }
    }
}
