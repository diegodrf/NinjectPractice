using Ninject.Modules;
using System;
using System.Net.Http;

namespace NinjectPractice.App_Start.DI
{
    public class HttpClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<HttpClient>()
                .ToMethod(context =>
                {
                    var client = new HttpClient
                    {
                        BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
                    };
                    client.DefaultRequestHeaders.Clear();
                    return client;
                })
                .InSingletonScope();
        }
    }
}