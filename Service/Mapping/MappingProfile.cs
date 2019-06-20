using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.Model;
using Services.Models;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Request, RequestViewModel>()
                .ForMember(x => x.RequestNumber, opt => opt.MapFrom(s => s.RequestNumber))
                .ForMember(x => x.RequestCategoryName, opt => opt.MapFrom(s => s.RequestCategory.Name))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Item.Name))
                .ForMember(x => x.Code, opt => opt.MapFrom(s => s.Item.Code))
                //.ForMember(x => x.Brand, opt => opt.MapFrom(s => s.Item.Brand))
                //.ForMember(x => x.Model, opt => opt.MapFrom(s => s.Item.Model))
                //.ForMember(x => x.Specification, opt => opt.MapFrom(s => s.Item.Specification))
                //.ForMember(x => x.Dimension, opt => opt.MapFrom(s => s.Item.Dimension))
                .ForMember(x => x.Unit, opt => opt.MapFrom(s => s.Item.Unit))
                //.ForMember(x => x.Price, opt => opt.MapFrom(s => s.Item.Price))
               // .ForMember(x => x.Comments, opt => opt.MapFrom(s => s.Item.Comments))
                .ForMember(x => x.StockNumber, opt => opt.MapFrom(s => s.Item.StockNumber));

            CreateMap<RequestViewModel, Request>()
                .ForMember(dest => dest.RequestId, opt => opt.Ignore());

            CreateMap<Item, RequestViewModel>();

            CreateMap<RequestHeader, RequestHeaderViewModel>()
                .ForMember(x => x.RequestViewModels, opt => opt.MapFrom(s => s.Requests));
        }
    }
}
