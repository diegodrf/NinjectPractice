using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using NinjectPractice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NinjectPractice.App_Start.DI
{
    public class DbContextModule : NinjectModule
    {
        public override void Load()
        {
            Bind<AppDbContext>()
               .ToMethod(context =>
               {
                   var dbContextName = "ProductsDbContext";
                   return new AppDbContext(dbContextName);
               })
               .InRequestScope();
        }
    }
}