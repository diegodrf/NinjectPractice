using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace NinjectPractice.Middlewares
{
    public class ErrorHandlerMiddleware : OwinMiddleware
    {
        public ErrorHandlerMiddleware(OwinMiddleware next) : base(next) { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                // LOG
            }

        }
    }
}