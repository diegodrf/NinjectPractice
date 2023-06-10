using Ninject.Web.WebApi;
using Ninject;
using NinjectPractice.App_Start;
using NinjectPractice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace NinjectPractice
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Products", action = "GetAll", id = UrlParameter.Optional }
            );
        }
    }
}
