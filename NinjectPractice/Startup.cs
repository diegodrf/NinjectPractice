using Microsoft.Owin;
using Ninject;
using Ninject.Modules;
using NinjectPractice.App_Start.DI;
using NinjectPractice.Middlewares;
using Owin;

[assembly: OwinStartup(typeof(NinjectPractice.Startup))]

namespace NinjectPractice
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = new StandardKernel(BindModules());
            
            app.UseNinject(() => kernel);

            app.Use(typeof(ErrorHandlerMiddleware));
            app.Use(typeof(RequestIdGeneratorMiddleware));
            
        }

        private INinjectModule[] BindModules()
        {
            var modules = new INinjectModule[]
            {
                new DbContextModule(),
                new RepositoriesModule(),
                new HttpClientModule()
            };
            return modules;
        }
    }
}