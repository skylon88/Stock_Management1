using NewServices;
using NewServices.Interfaces;
using NewServices.Services;

namespace WebAPI
{
    public static class DependencyInjectorRegister
    {
        public static void Init()
        {
            DependencyInjector.Register<IRequestService, RequestService>();
            DependencyInjector.Register<IManagementService, ManagementService>();
            DependencyInjector.Register<IPurchaseService, PurchaseService>();
            DependencyInjector.Register<IStockService, StockService>();
            DependencyInjector.AddExtension<DependencyOfDependencyExtension>();
        }
    }
}