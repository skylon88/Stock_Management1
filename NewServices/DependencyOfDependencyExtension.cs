using AutoMapper;
using Core.Data;
using Core.Repository;
using Core.Repository.Interfaces;
using NewServices.Mapping;
using Unity;
using Unity.Extension;

namespace NewServices
{
    public class DependencyOfDependencyExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            Container.RegisterType<IRequestHeaderRepository, RequestHeaderRepository>();
            Container.RegisterType<IRequestRepository, RequestRepository>();

            Container.RegisterType<IPurchaseApplicationHeaderRepository, PurchaseApplicationHeaderRepository>();
            Container.RegisterType<IPurchaseApplicationRepository, PurchaseApplicationRepository>();

            Container.RegisterType<IPurchaseHeaderRepository, PurchaseHeaderRepository>();
            Container.RegisterType<IPurchaseRepository, PurchaseRepository>();

            Container.RegisterType<IInStockHeaderRepository, InStockHeaderRepository>();
            Container.RegisterType<IInStockRepository, InStockRepository>();

            Container.RegisterType<IOutStockHeaderRepository, OutStockHeaderRepository>();
            Container.RegisterType<IOutStockRepository, OutStockRepository>();

            Container.RegisterType<IItemRepository, ItemRepository>();
            Container.RegisterType<ISupplierRepository, SupplierRepository>();
            Container.RegisterType<IPositionRepository, PositionRepository>();
            Container.RegisterType<IPoRepository, PoRepository>();
            Container.RegisterType<IContractRepository, ContractRepository>();
            Container.RegisterType<IFixingRepository, FixingRepository>();
            Container.RegisterType<IUnitRepository, UnitRepository>();
            //Container.RegisterType<IRequestService, RequestService>();

            Container.RegisterInstance(config.CreateMapper());
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }
}
                                                