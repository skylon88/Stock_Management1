using System.Linq;
using AutoMapper;
using Core.Enum;
using Core.Model;
using NewServices.Models.入库部分;
using NewServices.Models.出库部分;
using NewServices.Models.管理;
using NewServices.Models.采购部分;
using NewServices.Models.需求部分;

namespace NewServices.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Request, RequestViewModel>()
                .ForMember(x => x.RequestNumber, opt => opt.MapFrom(s => s.RequestNumber))
                .ForMember(x => x.RequestCategory, opt => opt.MapFrom(s => s.RequestHeader.RequestCategory))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Item.ChineseName))
                .ForMember(x => x.Code, opt => opt.MapFrom(s => s.Item.Code))
                .ForMember(x => x.Priority, opt => opt.MapFrom(s => s.Priority.Id))
                .ForMember(x => x.Max, opt => opt.MapFrom(s => s.Item.Max))
                .ForMember(x => x.ToApplyTotal, opt => opt.MapFrom(s => s.Total - s.Item.Positions.Sum(x => x.Total) > 0 ?
                    s.Total - s.Item.Positions.Sum(x => x.Total):
                    0))
                .ForMember(x => x.TotalInStorage, opt => opt.MapFrom(s => s.Item.Positions.FirstOrDefault(x => x.PositionName == s.Item.DefaultPositionName).Total))
                .ForMember(x => x.Unit, opt => opt.MapFrom(s => s.Item.Unit))
                .ForMember(x => x.Status, opt => opt.MapFrom(s => s.Status))
                .ForMember(x => x.PositionName, opt => opt.MapFrom(s => s.Item.DefaultPositionName))
                .ForMember(x => x.PositionViewModels, opt => opt.MapFrom(s => s.Item.Positions))
                .ForMember(x => x.FixAddress, opt => opt.MapFrom(s => s.FixModel.Address))
                .ForMember(x => x.Contact, opt => opt.MapFrom(s => s.FixModel.Contact))
                .ForMember(x => x.Phone, opt => opt.MapFrom(s => s.FixModel.Phone))
                .ForMember(x => x.FixingPrice, opt => opt.MapFrom(s => s.FixModel.Price))
                .ForMember(x => x.FixingDays, opt => opt.MapFrom(s => s.FixModel.Days))
                .ForMember(x => x.FixingFinishDate, opt => opt.MapFrom(s => s.FixModel.FinishDate))      
                .ForMember(x => x.PoNumber, opt => opt.MapFrom(s => s.RequestHeader.Contract.PoModel != null ? s.RequestHeader.Contract.PoModel.PoNumber : string.Empty));

            CreateMap<RequestViewModel, Request>()
                .ForMember(dest => dest.RequestId, opt => opt.Ignore());

            CreateMap<Item, RequestViewModel>();

            CreateMap<RequestHeader, RequestHeaderViewModel>()
                .ForMember(x => x.PoNumber, opt => opt.MapFrom(s => s.Contract.PoModel != null ? s.Contract.PoModel.PoNumber : string.Empty))
                .ForMember(x => x.ContractId, opt => opt.MapFrom(s => s.Contract.ContractNumber))
                .ForMember(x => x.Status, opt => opt.MapFrom(s => s.Requests.OrderBy(x=>x.Status).FirstOrDefault().Status.ToString()))
                .ForMember(x => x.Total, opt => opt.MapFrom(s => s.Requests.Count))
                .ForMember(x => x.LockStatus, opt => opt.MapFrom(s => s.Requests.Any(z=>z.LockStatus == LockStatusEnum.未准备) ? LockStatusEnum.未准备 : LockStatusEnum.已准备))
                .ForMember(x => x.RequestViewModels, opt => opt.MapFrom(s => s.Requests));
            CreateMap<PoModel, PoNumberViewModel>()
                .ForMember(x => x.PoNumber, opt => opt.MapFrom(s => s.PoNumber))
                .ForMember(x => x.Contracts, opt => opt.MapFrom(s => s.Contracts));

            CreateMap<Contract, ContractViewModel>()
                .ForMember(x => x.RequestHeaderViewModels, opt => opt.MapFrom(s => s.RequestHeaders));
            CreateMap<ContractViewModel, Contract>();

            CreateMap<RequestHeader, PurchaseApplicationHeader>()
                .ForMember(x => x.RequestNumber, opt => opt.MapFrom(s => s.RequestHeaderNumber));

            CreateMap<PurchaseApplication, PurchaseApplicationViewModel>()
               .ForMember(x => x.RequestId, opt => opt.MapFrom(s => s.RequestId))
               .ForMember(x => x.ApplicationNumber, opt => opt.MapFrom(s => s.PurchaseApplicationNumber))
               .ForMember(x => x.RequestNumber, opt => opt.MapFrom(s => s.PurchaseApplicationHeader.RequestNumber))
               .ForMember(x => x.ContractNo, opt => opt.MapFrom(s => s.PurchaseApplicationHeader.RequestHeader.Contract.ContractNumber))
               .ForMember(x => x.PoNumber, opt => opt.MapFrom(s => s.SelectedPONumber ?? s.PurchaseApplicationHeader.RequestHeader.Contract.PoModel.PoNumber))
               .ForMember(x => x.SupplierName, opt => opt.MapFrom(s => s.SupplierId))
               .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Item.ChineseName))
               .ForMember(x => x.Code, opt => opt.MapFrom(s => s.Item.Code))
               .ForMember(x => x.Category, opt => opt.MapFrom(s => s.Item.Category))
               .ForMember(x => x.Brand, opt => opt.MapFrom(s => s.Item.Brand))
               .ForMember(x => x.Model, opt => opt.MapFrom(s => s.Item.Model))
               .ForMember(x => x.Specification, opt => opt.MapFrom(s => s.Item.Specification))
               .ForMember(x => x.Dimension, opt => opt.MapFrom(s => s.Item.Dimension))
               .ForMember(x => x.Unit, opt => opt.MapFrom(s => s.Item.Unit))
               .ForMember(x => x.SupplierName, opt => opt.MapFrom(s => string.IsNullOrEmpty(s.SupplierId) ? s.Item.FirstSupplier : s.SupplierId))
               .ForMember(x => x.TotalApplied, opt => opt.MapFrom(s => s.TotalApplied)).ForMember(x => x.CurrentPurchasePrice, opt => opt.MapFrom(s => s.CurrentPurchasePrice))
               .ForMember(x => x.ItemId, opt => opt.MapFrom(s => s.ItemId))
               .ForMember(x => x.RequestCategory, opt => opt.MapFrom(s => s.PurchaseApplicationHeader.RequestCategory));

            CreateMap<PurchaseApplicationViewModel,PurchaseApplication>();

            CreateMap<PurchaseApplicationHeader, PurchaseApplicationHeaderViewModel>()
                .ForMember(x => x.PurchaseApplicationViewModels, opt => opt.MapFrom(s => s.PurchaseApplications))
                .ForMember(x => x.AuditStatus, opt => opt.MapFrom(s => s.PurchaseApplications.OrderBy(x => x.AuditStatus).FirstOrDefault().AuditStatus.ToString()))
                .ForMember(x => x.Status, opt => opt.MapFrom(s => s.PurchaseApplications.OrderBy(x => x.ProcessStatus).FirstOrDefault().ProcessStatus.ToString()))
                .ForMember(x => x.ApplicationNumber, opt => opt.MapFrom(s => s.PurchaseApplicationNumber))
                .ForMember(x => x.PoNumber, opt => opt.MapFrom(s => s.RequestHeader.Contract.PoModel.PoNumber))
                .ForMember(x => x.TotalConfirmed, opt => opt.MapFrom(s => s.PurchaseApplications.Count(x=>x.AuditStatus == AuditStatusEnum.已审批)))
                .ForMember(x => x.TotalApplied, opt => opt.MapFrom(s => s.PurchaseApplications.Count()))
                .ForMember(x => x.RequestHeaderNumber, opt => opt.MapFrom(s => s.RequestNumber));

            CreateMap<RequestViewModel, PurchaseApplication>()
                .ForMember(x => x.RequestId, opt => opt.MapFrom(s => s.RequestId))
                .ForMember(x => x.TotalApplied, opt => opt.MapFrom(s => s.ToApplyTotal))
                .ForMember(x => x.TotalConfirmed, opt => opt.MapFrom(s => s.ToApplyTotal))
                .ForMember(x => x.ItemId, opt => opt.MapFrom(s => s.ItemId));

            CreateMap<Item, ItemViewModel>().ForMember(x => x.TotalInStorage, opt => opt.MapFrom(s => s.Positions.Sum((x=>x.Total))))
                .ForMember(x => x.PositionViewModels, opt => opt.MapFrom(s => s.Positions));

            CreateMap<ItemViewModel, Item>();

            CreateMap<Position, PositionViewModel>();

            CreateMap<PositionViewModel, Position>()
               .ForMember(x => x.PositionName, opt => opt.MapFrom(s => s.PositionName))
               .ForMember(x => x.Total, opt => opt.MapFrom(s => s.Total));

            CreateMap<PurchaseApplicationHeader, PurchaseHeader>();

            CreateMap<PurchaseHeader, PurchaseHeaderViewModel>()
                .ForMember(x => x.PurchaseViewModels, opt => opt.MapFrom(s => s.Purchases))
                .ForMember(x => x.Priority, opt => opt.MapFrom(s => s.Purchases.FirstOrDefault().PurchaseApplication.Priority))
                .ForMember(x => x.SupplierName, opt => opt.MapFrom(s => s.SupplierId))
                .ForMember(x=>x.DeliveryDate, opt=>opt.MapFrom(s=>s.Purchases.OrderByDescending(x=>x.DeliveryDate).FirstOrDefault().DeliveryDate));


            CreateMap<PurchaseHeader, InStockHeader>()
                .ForMember(x => x.PurchaseNumber, opt => opt.MapFrom(s => s.PurchaseNumber))
                .ForMember(x => x.RequestNumber, opt => opt.MapFrom(s => s.Purchases.FirstOrDefault().PurchaseApplication.PurchaseApplicationHeader.RequestNumber));
            CreateMap<RequestHeader, InStockHeader>()
                 .ForMember(x => x.RequestNumber, opt => opt.MapFrom(s => s.RequestHeaderNumber));

            CreateMap<Purchase, PurchaseViewModel>()
                .ForMember(x => x.PoNumber, opt => opt.MapFrom(s => s.PurchaseApplication.PurchaseApplicationHeader.RequestHeader.Contract.PoModel.PoNumber))
                .ForMember(x => x.RequestId, opt => opt.MapFrom(s => s.PurchaseApplication.RequestId))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.PurchaseApplication.Item.ChineseName))
                .ForMember(x => x.SupplierCode, opt => opt.MapFrom(s => s.PurchaseHeader.SupplierId))
                .ForMember(x => x.ApplicationNumber, opt => opt.MapFrom(s => s.PurchaseApplication.PurchaseApplicationNumber))
                .ForMember(x => x.Code, opt => opt.MapFrom(s => s.PurchaseApplication.Item.Code))
                .ForMember(x => x.Category, opt => opt.MapFrom(s => s.PurchaseApplication.Item.Category))
                .ForMember(x => x.Brand, opt => opt.MapFrom(s => s.PurchaseApplication.Item.Brand))
                .ForMember(x => x.Model, opt => opt.MapFrom(s => s.PurchaseApplication.Item.Model))
                .ForMember(x => x.Specification, opt => opt.MapFrom(s => s.PurchaseApplication.Item.Specification))
                .ForMember(x => x.Dimension, opt => opt.MapFrom(s => s.PurchaseApplication.Item.Dimension))
                .ForMember(x => x.Unit, opt => opt.MapFrom(s => s.PurchaseApplication.Item.Unit))
                .ForMember(x => x.DefaultPrice, opt => opt.MapFrom(s => s.PurchaseApplication.Item.Price))
                .ForMember(x => x.Price, opt => opt.MapFrom(s => s.CurrentPurchasePrice))
                .ForMember(x => x.TotalPrice, opt => opt.MapFrom(s => s.CurrentPurchasePrice * s.PurchaseTotal))
                .ForMember(x => x.Note, opt => opt.MapFrom(s => s.Note))
                .ForMember(x => x.Position, opt => opt.MapFrom(s => s.PurchaseApplication.Item.Positions.FirstOrDefault(x => x.PositionName == "Stage").PositionName))
                 //.ForMember(x => x.AlreadyInStock, opt => opt.MapFrom(s => s.InStocks.Sum(i => i.Total)))
                 .ForMember(x => x.ReadyForInStock, opt => opt.MapFrom(s => s.ReadyForInStock))
                .ForMember(x => x.IsPriceChange, opt => opt.MapFrom(s => s.IsPriceChange))
                .ForMember(x => x.ItemId, opt => opt.MapFrom(s => s.PurchaseApplication.Item.ItemId));

            CreateMap<PurchaseViewModel, InStock>()
                .ForMember(x => x.Total, opt => opt.MapFrom(s => s.ReadyForInStock))
                .ForMember(x => x.PurchaseId, opt => opt.MapFrom(s => s.PurchaseId));
            //.ForMember(x => x.ItemId, opt => opt.MapFrom(s => s.ItemId));

            CreateMap<RequestViewModel, InStock>()
                .ForMember(x => x.Total, opt => opt.MapFrom(s => s.ToInStockTotal))
                .ForMember(x => x.ItemId, opt => opt.MapFrom(s => s.ItemId));



            CreateMap<InStockHeader, InStockHeaderViewModel>()
                //.ForMember(x => x.PurchaseNumber, opt => opt.MapFrom(s => s.InStocks.FirstOrDefault().Purchase.PurchaseNumber))
                //.ForMember(x => x.RequestNumber, opt => opt.MapFrom(s => s.InStocks.FirstOrDefault().Purchase.PurchaseApplication.PurchaseApplicationHeader.RequestNumber))
                //.ForMember(x => x.SupplierName, opt => opt.MapFrom(s => s.InStocks.FirstOrDefault().Purchase.PurchaseApplication.SupplierId))
                .ForMember(x => x.InStockViewModels, opt => opt.MapFrom(s => s.InStocks))
                .ForMember(x => x.InStockCategory, opt => opt.MapFrom(s => s.InStocks.FirstOrDefault().Type));

            CreateMap<InStock, InStockViewModel>()
                .ForMember(x => x.ProcessStatus, opt => opt.MapFrom(s => s.Status))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Item.ChineseName))
                .ForMember(x => x.Code, opt => opt.MapFrom(s => s.Item.Code))
                .ForMember(x => x.Brand, opt => opt.MapFrom(s => s.Item.Brand))
                .ForMember(x => x.Model, opt => opt.MapFrom(s => s.Item.Model))
                .ForMember(x => x.Specification, opt => opt.MapFrom(s => s.Item.Specification))
                .ForMember(x => x.Dimension, opt => opt.MapFrom(s => s.Item.Dimension))
                .ForMember(x => x.Unit, opt => opt.MapFrom(s => s.Item.Unit));
            //.ForMember(x => x.Price, opt => opt.MapFrom(s => s.Purchase.CurrentPurchasePrice))
            //.ForMember(x => x.TotalPrice, opt => opt.MapFrom(s => s.Total * s.Purchase.PurchaseApplication.Item.Price));

            CreateMap<OutStockHeader, OutStockHeaderViewModel>()
                .ForMember(x => x.ContractNumber, opt => opt.MapFrom(s => s.RequestHeader.Contract.ContractNumber))
                .ForMember(x => x.Address, opt => opt.MapFrom(s => s.RequestHeader.Contract.Address))
                .ForMember(x => x.OutStockNumber, opt => opt.MapFrom(s => s.OutStockHeaderNumber))
                .ForMember(x => x.OutStockNumber, opt => opt.MapFrom(s => s.OutStockHeaderNumber));
                //.ForMember(x => x.OutStockCategory, opt => opt.MapFrom(s => s.OutStocks.FirstOrDefault().Type))
                //.ForMember(x => x.OutStockViewModels, opt => opt.MapFrom(s => s.OutStocks));

            CreateMap<OutStock, OutStockViewModel>()
                .ForMember(x => x.ProcessStatus, opt => opt.MapFrom(s => s.Status))
                .ForMember(x => x.OutStockNumber, opt => opt.MapFrom(s => s.OutStockNumber))
                .ForMember(x => x.Type, opt => opt.MapFrom(s => s.Type))
                //.ForMember(x => x.Name, opt => opt.MapFrom(s => s.Request.Item.ChineseName))
                //.ForMember(x => x.Code, opt => opt.MapFrom(s => s.Request.Item.Code))
                //.ForMember(x => x.Brand, opt => opt.MapFrom(s => s.Request.Item.Brand))
                //.ForMember(x => x.Model, opt => opt.MapFrom(s => s.Request.Item.Model))
                //.ForMember(x => x.Specification, opt => opt.MapFrom(s => s.Request.Item.Specification))
                //.ForMember(x => x.Dimension, opt => opt.MapFrom(s => s.Request.Item.Dimension))
                //.ForMember(x => x.Unit, opt => opt.MapFrom(s => s.Request.Item.Unit))
                //.ForMember(x => x.Price, opt => opt.MapFrom(s => s.Price != 0 ? s.Price : s.Request.Item.Price))
                .ForMember(x => x.TotalPrice, opt => opt.MapFrom(s => s.Total * s.Price));

            CreateMap<RequestHeader, OutStockHeader>();

            CreateMap<RequestViewModel, OutStock>()
                .ForMember(x => x.Total, opt => opt.MapFrom(s => s.ToOutStockTotal))
                .ForMember(x => x.RequestId, opt => opt.MapFrom(s => s.RequestId));

            CreateMap<SupplierViewModel, Supplier>();

        }
    }
}
