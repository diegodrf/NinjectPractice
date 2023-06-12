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
                   LoadCurrentPathToConnectionString();
                   var dbContextName = "ProductsDbContext";
                   return new AppDbContext(dbContextName);
               })
               .InRequestScope();
        }

        /// <summary>
        /// Method to change the placeholder |DataDirectory| 
        /// in ConnectionString Web.config to runtime current path.
        /// </summary>
        private void LoadCurrentPathToConnectionString()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }
    }
}