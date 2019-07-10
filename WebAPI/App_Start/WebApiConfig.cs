using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using NewServices;
using NewServices.Interfaces;
using NewServices.Services;
using Unity;
using WebAPI.Controllers;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IRequestService, RequestService>();
            container.RegisterType<IManagementService, ManagementService>();
            container.RegisterType<IPurchaseService, PurchaseService>();
            container.RegisterType<IStockService, StockService>();
            container.AddNewExtension<DependencyOfDependencyExtension>();
            ManagementService managementService = container.Resolve<ManagementService>();
            new DatabaseConfig(managementService);
            container.Resolve<RequestHeaderController>();

            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
