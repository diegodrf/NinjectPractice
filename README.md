## A simple project to practice the use of Ninject to implements IoC

#### Attention
- If you need to run this project, pay attention to `NinjectPractice/Web.config` file. The path for datasource is hardcoded.
```xml
<connectionStrings>
    <add name="ProductsDbContext" connectionString="Data Source=C:\Users\diego\source\repos\NinjectPractice\NinjectPractice\ProductsDbContext.db;Version=3" providerName="System.Data.SQLite" />
  </connectionStrings>
```

### How to implement Ninject
- Install packages
    - `Ninject.Web.WebApi`
    - `Ninject.Web.WebApi.WebHost`. This packege will create a file called `NinjectWebCommon.cs` in `App_Start` folder.
- You can just insert your DI into `NinjectWebCommon.RegisterServices` method and that's it.
```csharp
private static void RegisterServices(IKernel kernel) {
    kernel.Bind<IProductsRepository>().To<IProductsRepository>();
}
```

### If you need more organization
- Create a class that hinherit from abstract class `NinjectModule` and implements the method `Load()`
- Put your DI into `Load()` method. *Note that it uses the property `Bind` directly without the prefix `kernel`*
```csharp
using Ninject.Modules;
using Ninject.Web.Common;
using NinjectPractice.Repositories;

namespace NinjectPractice.App_Start.DI
{
    public class RepositoriesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductsRepository>().To<ProductsRepository>().InRequestScope();
        }
    }
}
```
- Instantiate your class into `StandardKernel` instatiation in `NinjectWebCommon.cs`.
```csharp
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectPractice.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectPractice.App_Start.NinjectWebCommon), "Stop")]

namespace NinjectPractice.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using NinjectPractice.App_Start.DI;
    using NinjectPractice.Repositories;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(
                new DbContextModule(),
                new RepositoriesModule()
                );
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel) {}
    }
}
```

### Sources
- https://github.com/ninject/Ninject/wiki
    - https://github.com/ninject/Ninject/wiki/Object-Scopes
    - https://github.com/ninject/Ninject/wiki/Modules-and-the-Kernel
    - https://github.com/ninject/Ninject/wiki/Providers%2C-Factory-Methods-and-the-Activation-Context
