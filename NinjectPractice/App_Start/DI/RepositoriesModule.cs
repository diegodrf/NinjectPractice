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