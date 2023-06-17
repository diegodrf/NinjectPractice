using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NinjectPractice.Middlewares
{
    public class RequestIdGeneratorMiddleware : OwinMiddleware
    {
        private readonly string _requestId;

        public RequestIdGeneratorMiddleware(OwinMiddleware next) : base(next)
        {
            _requestId = Guid.NewGuid().ToString("D");
        }

        public override async Task Invoke(IOwinContext context)
        {
            var requestIdHeader = new KeyValuePair<string, string[]>("RequestId", new string[] { _requestId });

            context.Request.Headers.Add(requestIdHeader);

            await Next.Invoke(context);

            context.Response.Headers.Add(requestIdHeader);
        }
    }
}