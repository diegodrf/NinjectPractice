using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using NinjectPractice.Repositories;
using NinjectPractice.Services;

namespace NinjectPractice.App_Start.DI
{
    public class RepositoriesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOptions<MemoryCacheOptions>>()
                .To<MemoryCacheOptions>()
                .InSingletonScope()
                .WithConstructorArgument("CompactionPercentage", .25)
                .WithConstructorArgument("SizeLimit", 1024);

            Bind<IMemoryCache>()
                .To<MemoryCache>()
                .InSingletonScope();

            Bind<ProductsRepository>()
                .ToSelf()
                .InRequestScope();

            Bind<IProductsRepository>()
                .To<ProductsRepositoryCached>()
                .InRequestScope()
                .WithConstructorArgument<IProductsRepository>(Kernel.Get<ProductsRepository>());

            Bind<IJsonPlaceholderService>()
                .To<JsonPlaceholderService>()
                .InRequestScope();
        }
    }
}