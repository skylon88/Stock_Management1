using System;
using AutoMapper;
using Core.Data;
using Core.Repository;
using Core.Repository.Interface;
using Services.Mapping;
using Unity;
using Unity.Extension;

namespace Services
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
            Container.RegisterInstance(config.CreateMapper());
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }
}
                                                