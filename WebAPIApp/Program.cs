using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewServices;
using NewServices.Services;
using WebAPIApp.Controllers;

namespace WebAPIApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Register DependencyInjector 
            DependencyInjectorRegister.Init();
           
            ManagementService managementService = DependencyInjector.Retrieve<ManagementService>();
            var ctx = new DatabaseConfig(managementService);
            DependencyInjector.Retrieve<RequestController>();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
