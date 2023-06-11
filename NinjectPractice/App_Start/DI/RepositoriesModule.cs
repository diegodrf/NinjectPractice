using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using NinjectPractice.Repositories;

namespace NinjectPractice.App_Start.DI
{
    public class RepositoriesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMemoryCache>()
                .To<MemoryCache>()
                .InSingletonScope()
                .WithConstructorArgument<IOptions<MemoryCacheOptions>>(new MemoryCacheOptions());

            Bind<ProductsRepository>()
                .ToSelf()
                .InRequestScope();

            Bind<IProductsRepository>()
                .To<ProductsRepositoryCached>()
                .InRequestScope()
                .WithConstructorArgument<IProductsRepository>(Kernel.Get<ProductsRepository>())
                .WithConstructorArgument<IMemoryCache>(Kernel.Get<IMemoryCache>());
        }
    }
}