using NewServices;
using NewServices.Interfaces;
using NewServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using WebAPI.Controllers;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            DependencyInjectorRegister.Init();
            ManagementService managementService = DependencyInjector.Retrieve<ManagementService>();
            var ctx = new DatabaseConfig(managementService);
            DependencyInjector.Retrieve<RequestHeadersController>();
            DependencyInjector.Retrieve<ValuesController>();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
