using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Resources;
using AutoMapper;
using Core.Enum;
using Core.Model;
using Core.Repository;
using Core.Repository.Interfaces;
using NewServices.Interfaces;
using NewServices.Models.管理;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace NewServices.Services
{
    public class ManagementService : IManagementService
    {
        private readonly IPoRepository _poRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IContractRepository _contractRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        string _resourceName = "Images.resources";
        public ManagementService(IPoRepository poRepository,
                                 IItemRepository itemRepository,
                                 IPositionRepository positionRepository,
                                 IContractRepository contractRepository,
                                 ISupplierRepository supplierRepository,
                                 IMapper mapper)
        {
            _poRepository = poRepository;
            _itemRepository = itemRepository;
            _positionRepository = positionRepository;
            _contractRepository = contractRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public IList<PoNumberViewModel> GetAllPoNumbers()
        {
            var query = _poRepository.GetAllPoModels().ToList();
            var models = _mapper.Map<PoNumberViewModel[]>(query);
            return models;
        }

        public ItemViewModel GetItemById(Guid id)
        {
            var query = _itemRepository.FindBy(x=>x.ItemId == id).FirstOrDefault();
            var models = _mapper.Map<ItemViewModel>(query);
            return models;
        }

        public IList<SupplierViewModel> GetAllSuppliers()
        {
            var query = _supplierRepository.GetAll();
            IList<SupplierViewModel> models = _mapper.Map<SupplierViewModel[]>(query);
            return models;
        }

        public IList<Supplier> GetSuppliers()
        {
            var query = _supplierRepository.GetAll().ToList();
            return query;
        }

        public IList<ItemViewModel> GetAllItems()
        {
            Dictionary<string, byte[]> resourceDictionary = new Dictionary<string, byte[]>();
            var query = _itemRepository.GetAll().Include(x => x.Positions).OrderBy(x => x.SerialNumber).ToList();
            ItemViewModel[] models = _mapper.Map<ItemViewModel[]>(query);          
            using (ResourceReader reader = new ResourceReader(_resourceName)) {

                foreach (DictionaryEntry item in reader)
                {
                    resourceDictionary.Add(item.Key.ToString(), (byte[]) item.Value);
                }

                foreach (var model in models)
                {
                    if (resourceDictionary.ContainsKey(model.Code))
                    {
                        model.Picture = resourceDictionary[model.Code];
                    }
                }
            }
            return models;
        }

        public void CreateContract(ContractViewModel model)
        {
            Contract newContract = _mapper.Map<Contract>(model);
            _contractRepository.Add(newContract);
            _contractRepository.Save();

        }

        public void UpdateItem(ItemViewModel model)
        {
            var item = _itemRepository.FindBy(x => x.ItemId == model.ItemId).FirstOrDefault();
            if (item == null) return;
            item.FirstSupplier = model.FirstSupplier;
            item.SecondSupplier = model.SecondSupplier;
            item.ThirdSupplier = model.ThirdSupplier;
            item.UpdateDate = DateTime.Now;
            try
            {
                _itemRepository.Edit(item);
                _itemRepository.Save();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void UpdateContract(ContractViewModel model)
        {
            var contract = _contractRepository.FindBy(x => x.Id == model.Id).FirstOrDefault();
            if (contract == null) return;
            contract.ContractNumber = model.ContractNumber;
            contract.Address = model.Address;
            contract.UpdateDate = DateTime.Now;
            try
            {
                _contractRepository.Edit(contract);
                _contractRepository.Save();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void UpdatePo(PoNumberViewModel model)
        {
            var po = _poRepository.FindBy(x => x.Id == model.Id).FirstOrDefault();
            if (po == null) return;
            po.PoNumber = model.PoNumber;
            po.UpdateDate = DateTime.Now;
            try
            {
                _poRepository.Edit(po);
                _poRepository.Save();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public int CreateSupplier(string name)
        {
            if (name == null) return 0;
            var isExisting = _supplierRepository.FindBy(x => x.Name == name).FirstOrDefault() !=null;
            if (!isExisting)
            {
                var newInstance = new Supplier()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Type = SupplierType.公寓
                };
                _supplierRepository.Add(newInstance);
                _supplierRepository.Save();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void UpdateSupplier(SupplierViewModel model)
        {
            var supplier = _supplierRepository.FindBy(x => x.Id == model.Id).FirstOrDefault();
            if (supplier == null) return;
            supplier.Name = model.Name;
            supplier.Type = model.Type;
            supplier.Code = model.Code;
            supplier.UpdateDate = DateTime.Now;
            try
            {
                _supplierRepository.Edit(supplier);
                _supplierRepository.Save();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool DeletePosition(PositionViewModel model)
        {
            var positionToDelete = _positionRepository.FindBy(x => x.Id == model.Id).FirstOrDefault();
            if (positionToDelete == null) return false;
            _positionRepository.Delete(positionToDelete);
            _positionRepository.Save();
            return true;
        }

        public ContractViewModel GetContractByContractId(string contractNumber)
        {
            var model = _contractRepository.FindBy(x => x.ContractNumber == contractNumber).FirstOrDefault();
            if (model == null) return null;
            ContractViewModel result = _mapper.Map<ContractViewModel>(model);
            return result;
        }


        public void InitalImagesToLocal(string resourceName, ExcelWorksheet worksheet)
        {
                using (ResourceWriter rw = new ResourceWriter(resourceName))
                {
                    var end = worksheet.Dimension.End;
                    for (var row = 9; row < end.Row; row++)
                    {
                        var code = worksheet.Cells[row, 3].Value == null ? "" : worksheet.Cells[row, 3].Value.ToString();
                        //Loading Picture
                        if (worksheet.Drawings["image" + row] is ExcelPicture loadPicture)
                        {
                            var uploadPicture = ImageToByteArray(loadPicture.Image);
                            rw.AddResource(code, uploadPicture);
                            
                        }
                    }
                    rw.Generate();
                }

        }

        public int Upload(string fileName, out string returnMsg, bool isInitial = false)
        {
            ExcelPackage package;
            IList<Item> listOfItems = new List<Item>();
            returnMsg = string.Empty;

            //string strorageconn = System.Configuration.ConfigurationSettings.AppSettings.Get("StorageConnectionString");
            //CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);
            //CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
            //CloudBlobContainer container = blobClient.GetContainerReference("itemimages");

            if (isInitial)
            {
                var excelFile = Resource.仓库物品资料汇总表;
                var templateStream = new MemoryStream(excelFile);
                package = new ExcelPackage(templateStream);
            }
            else
            {
                if (string.IsNullOrEmpty(fileName)) return -2;
                var fi = new FileInfo(fileName);
                package = new ExcelPackage(fi);
            }
            var workbook = package.Workbook;
            var worksheet = workbook.Worksheets["物品资料汇总表"];
            if (worksheet == null) return 0;
            InitalImagesToLocal(_resourceName, worksheet);
            var end = worksheet.Dimension.End;
            for (var row = 9; row < end.Row; row++)
            {
                var itemObject = new Item
                {
                    ItemId = Guid.NewGuid(),
                    DefaultPositionName = "Stage",
                    UpdateDate = DateTime.Now
                };
                for (var col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    var readValue = worksheet.Cells[row, col].Value == null ? "" : worksheet.Cells[row, col].Value.ToString();
                    switch (col)
                    {
                        case 1:
                            if (readValue == "") break;
                            else itemObject.SerialNumber = int.Parse(readValue);//序号
                            break;
                        case 3:
                            itemObject.Code = readValue; // 编号
                            break;
                        case 4:
                            itemObject.Status = readValue;  //状态
                            if (readValue.Contains("Active")) itemObject.Status = "现行Active";
                            else if (readValue.Contains("Discountinue")) itemObject.Status = "中止Discountinue";
                            else if (readValue.Contains("Cancel")) itemObject.Status = "取消Cancel";
                            else if (readValue.Contains("Deactive")) itemObject.Status = "下线Deactive";
                            else itemObject.Status = "";
                            break;
                        case 5:
                            itemObject.Category = readValue; //物品类别
                            break;
                        case 6:
                            itemObject.ProjectCategory = readValue; //所属转项类
                            break;
                        case 7:
                            itemObject.SubCategory = readValue;//物品种类
                            break;
                        case 8:
                            itemObject.BigCategory = readValue; //物品大类
                            break;
                        case 9:
                            itemObject.SmallCategory = readValue;//物品小类
                            break;
                        case 10:
                            itemObject.DetailCategory = readValue; //物品细类
                            break;

                        case 11:
                            itemObject.AdjustCategory = readValue; //调整类
                            break;
                        case 12:
                            itemObject.Attribute = readValue;//物品属性
                            break;
                        case 13:
                            itemObject.Property = readValue;//资产类型
                            break;
                        case 14:
                            itemObject.ChineseName = readValue.Replace("\n", " ");//中文名
                            break;
                        case 15:
                            itemObject.EnglishName = readValue.Replace("\n", " ");//英文名
                            break;
                        case 16:
                            break;
                        case 17:
                            itemObject.Brand = readValue; //品牌
                            break;
                        case 18:
                            itemObject.Model = readValue; //型号
                            break;
                        case 19:
                            itemObject.Specification = readValue; //规格
                            break;
                        case 20:
                            itemObject.Dimension = readValue; //尺寸
                            break;
                        case 21:
                            itemObject.Length = readValue; //长
                            break;
                        case 22:
                            itemObject.Width = readValue; //宽
                            break;
                        case 23:
                            itemObject.Height = readValue; //高
                            break;
                        case 25:
                            itemObject.Unit = readValue; //单位
                            break;
                        case 26:
                            if (readValue == "") itemObject.Price = 0;
                            else itemObject.Price = double.Parse(readValue);//单价
                            break;
                        case 27:
                            itemObject.Package = readValue; //包装数量
                            break;
                        case 28:
                            itemObject.PackageLength = readValue; //包装长
                            break;
                        case 29:
                            itemObject.PackageWidth = readValue; //包装宽
                            break;
                        case 30:
                            itemObject.PackageHeight = readValue; //包装高
                            break;
                        case 31:
                            itemObject.Detail = readValue; //详细信息链接
                            break;
                        case 35:
                            if (readValue == "") itemObject.Max = 0;
                            else itemObject.Max = double.Parse(readValue);//库存上限
                            break;
                        case 36:
                            if (readValue == "") itemObject.Min = 0;
                            else itemObject.Min = double.Parse(readValue);//库存下限
                            break;
                        case 38:
                            itemObject.FirstSupplier = readValue.TrimEnd().TrimStart(); // 一级供应商
                            break;
                        case 41:
                            itemObject.SecondSupplier = readValue.TrimEnd().TrimStart(); // 二级供应商
                            break;
                        case 44:
                            itemObject.ThirdSupplier = readValue.TrimEnd().TrimStart(); // 三级供应商
                            break;
                        case 48:
                            itemObject.CostCategory = readValue;
                            break;
                        case 50:
                            itemObject.ArrangeOrder = readValue;
                            break;
                        case 51:
                            itemObject.ArrangePosition = readValue;
                            break;
                    }

                    
                }

                if (itemObject.SerialNumber > 0)
                {
                    listOfItems.Add(itemObject);
                }
            }


            try
            {
                var existingItems = _itemRepository.GetAll();
                var existingPositions = _positionRepository.GetAll();

                if (!existingItems.Any())
                {
                    _itemRepository.CleanUp();
                    _itemRepository.AddRange(listOfItems);
                }
                else
                {
                    foreach (var item in listOfItems)
                    {
                        var existingItem = existingItems.FirstOrDefault(x => x.Code == item.Code);
                        if (existingItem != null)
                        {
                            existingItem.Status = item.Status;
                            existingItem.Category = item.Category;
                            existingItem.ProjectCategory = item.ProjectCategory;
                            existingItem.SubCategory = item.SubCategory;
                            existingItem.BigCategory = item.BigCategory;
                            existingItem.SmallCategory = item.SmallCategory;
                            existingItem.DetailCategory = item.DetailCategory;
                            existingItem.AdjustCategory = item.AdjustCategory;
                            existingItem.Attribute = item.Attribute;
                            existingItem.Property = item.Property;
                            existingItem.ChineseName = item.ChineseName;
                            existingItem.EnglishName = item.EnglishName;
                            existingItem.Brand = item.Brand;
                            existingItem.Model = item.Model;
                            existingItem.Specification = item.Specification;
                            existingItem.Dimension = item.Dimension;
                            existingItem.Length = item.Length;
                            existingItem.Width = item.Width;
                            existingItem.Height = item.Height;
                            existingItem.Unit = item.Unit;
                            existingItem.Price = item.Price;
                            existingItem.Package = item.Package;
                            existingItem.PackageLength = item.PackageLength;
                            existingItem.PackageWidth = item.PackageWidth;
                            existingItem.PackageHeight = item.PackageHeight;
                            existingItem.Detail = item.Detail;
                            existingItem.Max = item.Max;
                            existingItem.Min = item.Min;
                            existingItem.CostCategory = item.CostCategory;
                            existingItem.ArrangeOrder = item.ArrangeOrder;
                            existingItem.ArrangePosition = item.ArrangePosition;
                            existingItem.Comments = item.Comments;
                            existingItem.FirstSupplier = item.FirstSupplier;
                            existingItem.SecondSupplier = item.SecondSupplier;
                            existingItem.ThirdSupplier = item.ThirdSupplier;
                        }
                        else
                        {
                            var positionObject = new Position()
                            {
                                ItemId = item.ItemId,
                                Code = item.Code,
                                PositionName = "Stage"
                            };
                            _positionRepository.Add(positionObject);
                            _itemRepository.Add(item);
                        }
                    }
                }
                _itemRepository.Save();
                _positionRepository.Save();
                
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        private static byte[] ImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        public int UploadRelationship(string fileName, bool isInitial = false)
        {
            ExcelPackage package;
            IList<Item> listOfItems = _itemRepository.GetAll().ToList();
            IList<Position> existingOfPoistion = _positionRepository.GetAll().ToList();
            IList<Position> listOfPositions = new List<Position>();

            if (isInitial)
            {
                var excelFile = Resource.库位;
                var templateStream = new MemoryStream(excelFile);
                package = new ExcelPackage(templateStream);
                listOfPositions = listOfItems.Select(item => new Position() { ItemId = item.ItemId, Code = item.Code, PositionName = "Stage" }).ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(fileName)) return -2;
                var fi = new FileInfo(fileName);
                package = new ExcelPackage(fi);
            }
            var workbook = package.Workbook;
            ExcelWorksheet worksheet = workbook.Worksheets["库位关系"];
            if (worksheet == null) return 0;
            var end = worksheet.Dimension.End;
            for (var row = 3; row <= end.Row; row++)
            //for (var row = 3; row <= 40; row++)
            {
                var positionObject = new Position {Id = Guid.NewGuid()};
                
                for (var col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    var readValue = worksheet.Cells[row, col].Value == null ? "" : worksheet.Cells[row, col].Value.ToString();
                    switch (col)
                    {
                        case 2:
                            positionObject.Code = readValue;
                            var item = listOfItems.FirstOrDefault(x => x.Code == readValue);
                            if (item != null)
                            {
                                positionObject.ItemId = item.ItemId;
                                positionObject.Total = 0;
                            }
                            break;
                        case 5:
                            positionObject.PositionName = readValue;
                            break;
                        case 6:
                            positionObject.Area = readValue;
                            break;
                        case 7:
                            positionObject.Location = readValue;
                            break;
                    }

                }

                var isExisting = existingOfPoistion.FirstOrDefault(x => x.Code == positionObject.Code && x.PositionName == positionObject.PositionName);

                if (isExisting == null && positionObject.ItemId != Guid.Empty)
                {
                    listOfPositions.Add(positionObject);
                }
            }

            try
            {

                _positionRepository.AddRange(listOfPositions);
                _positionRepository.Save();
                return 1;
            }
            catch (DbUpdateException e)
            {
                var msg = e.Message;
                return 0;
            }

        }

        public int UploadSuppliers(string fileName, bool isInitial = false)
        {
            ExcelPackage package;
            IList<Supplier> listOfSupplier = new List<Supplier>();
                      
            if (isInitial)
            {
                byte[] excelFile = Resource.仓库物品资料汇总表;
                var templateStream = new MemoryStream(excelFile);
                package = new ExcelPackage(templateStream);
            }
            else
            {
                if (string.IsNullOrEmpty(fileName)) return -2;
                var fi = new FileInfo(fileName);
                package = new ExcelPackage(fi);
            }
            var workbook = package.Workbook;
            ExcelWorksheet worksheet = workbook.Worksheets["供应商信息表Vendor Master"];//供应商信息表Vendor Master
            if (worksheet == null) return 0;
            var end = worksheet.Dimension.End;
            for (var row = 6; row < end.Row; row++)
            {
                var supplierObj = new Supplier {Id = Guid.NewGuid()};
                for (var col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    var readValue = worksheet.Cells[row, col].Value == null ? "" : worksheet.Cells[row, col].Value.ToString();
                    switch (col)
                    {
                        case 2:
                            supplierObj.IsActive = readValue == "Active";
                            break;
                        case 3:
                            supplierObj.Name = readValue;
                            break;
                    }

                }
                listOfSupplier.Add(supplierObj);
            }

            try
            {
                _supplierRepository.AddRange(listOfSupplier);
                _supplierRepository.Save();
                return 1;
            }
            catch (DbUpdateException)
            {
                return 0;
            }

        }

        public Guid GetPoNumberIfNotExisting(string poNumber)
        {
            var isExisting = _poRepository.FindBy(x => x.PoNumber == poNumber).FirstOrDefault();

            if(isExisting != null)
            {
                return isExisting.Id;
            }
            else
            {
                var newPoInstance = new PoModel()
                {
                    Id = Guid.NewGuid(),
                    PoNumber = poNumber
                };
                _poRepository.Add(newPoInstance);
                _poRepository.Save();
                return newPoInstance.Id;
            }
        }

        public IList<string> GetPositionNameByCode(string code)
        {
            IList<string> listOfPositionNames = code == null ? _positionRepository.GetAll().Select(y => y.PositionName).ToList() : _positionRepository.FindBy(x => x.Code == code).Select(y=>y.PositionName).ToList();
            return listOfPositionNames;
        }

        public void UpdateStorage(string code, string positionName, double total, string inStockNumber)
        {
            var position = _positionRepository.FindBy(x => x.Code == code && x.PositionName == positionName).FirstOrDefault();
            if (position == null) return;
            position.Total = position.Total + total;
            if(position.Total < 0)
            {
                position.Total = 0;
            }
            position.UpdateDate = DateTime.Now;
            position.LatestInStockNumber = inStockNumber;
            _positionRepository.Edit(position);
            _positionRepository.Save();
        }

        public bool TransferStorage(PositionViewModel afterModifiedPosition)
        {
            var allPositions = _positionRepository.FindBy(x => x.ItemId == afterModifiedPosition.ItemId);
            var beforeModifiedPosition = allPositions.FirstOrDefault(x => x.Id == afterModifiedPosition.Id);
            var stagePosition = allPositions.FirstOrDefault(x => x.PositionName == "Stage");
            var restPositions = allPositions.Where(x => x.Id != afterModifiedPosition.Id && x.PositionName != "Stage").ToList();
            var totalStock = allPositions.Sum(x => x.Total);
            if (beforeModifiedPosition == null) return false;
            var diffTotal = afterModifiedPosition.Total - beforeModifiedPosition.Total;
            if (afterModifiedPosition.Total > totalStock) return false;
            while (diffTotal > 0)
            {
                if(stagePosition != null && stagePosition.Total > 0)
                {
                    var stagePositionTotalTemp = stagePosition.Total;
                    var diffTotalTemp = diffTotal;
                    stagePosition.Total = stagePositionTotalTemp - diffTotalTemp >= 0 ? stagePositionTotalTemp - diffTotalTemp : 0;
                    diffTotal = stagePositionTotalTemp - diffTotalTemp >= 0 ? 0 : diffTotalTemp - stagePositionTotalTemp;
                }
                else
                {
                    foreach(var restPosition in restPositions)
                    {
                        if (restPosition.Total <= 0) continue;
                        var restPositionTotalTemp = restPosition.Total;
                        var diffTotalTemp = diffTotal;
                        restPosition.Total = restPositionTotalTemp - diffTotalTemp >= 0 ? restPositionTotalTemp - diffTotalTemp : 0;
                        diffTotal = restPositionTotalTemp - diffTotalTemp >= 0 ? 0 : diffTotalTemp - restPositionTotalTemp;
                    }
                }
            }
            if(diffTotal == 0)
            {
                beforeModifiedPosition.Total = afterModifiedPosition.Total;
            }
            else if(diffTotal < 0)
            {
                if (afterModifiedPosition.PositionName != "Stage")
                {
                    if (stagePosition != null) stagePosition.Total = stagePosition.Total - diffTotal;
                    beforeModifiedPosition.Total = afterModifiedPosition.Total;
                }
            }
            beforeModifiedPosition.UpdateDate = DateTime.Now;
            _positionRepository.Edit(beforeModifiedPosition);
            _positionRepository.Edit(stagePosition);
            _positionRepository.EditRange(restPositions);
            _positionRepository.Save();
            return true;
        }

        public void UpdateItemPrice(Guid itemId, double price)
        {
            var item = _itemRepository.FindBy(x => x.ItemId == itemId).FirstOrDefault();
            if (item != null)
            {
                item.Price = Math.Round((item.Price + price) / 2, 2);
                item.UpdateDate = DateTime.Now;
                _itemRepository.Edit(item);
            }

            _itemRepository.Save();
        }

        public bool ExportItemExcel(string path, ReportNameEnum reportType, DateTime selectedMonth)
        {
            var excelFile = Resource.采购申请表;
            using (ExcelPackage newpackage = new ExcelPackage())
            {
                if (reportType == ReportNameEnum.物品盘点)
                {
                    using (var templateStream = new MemoryStream(excelFile))
                    {
                        using (ExcelPackage package = new ExcelPackage(templateStream))
                        {
                            var template = package.Workbook.Worksheets[ReportNameEnum.物品盘点.ToString()];
                            GenerateItemSummarySheet(template, true); //显示Position
                            newpackage.Workbook.Worksheets.Add("物品盘点(库位)", template);
                        }
                    }
                    using (var templateStream = new MemoryStream(excelFile))
                    {
                        using (ExcelPackage package = new ExcelPackage(templateStream))
                        {
                            var template = package.Workbook.Worksheets[ReportNameEnum.物品盘点.ToString()];
                            GenerateItemSummarySheet(template, false); //显示上限下限
                            newpackage.Workbook.Worksheets.Add("物品盘点(库存上下限)", template);
                        }
                    }
                }
                try
                {
                    FileInfo file = new FileInfo(path);
                    newpackage.SaveAs(file);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private void GenerateItemSummarySheet(ExcelWorksheet template, bool tab)
        {
            var count = 1;
            var row = 8;
            template.Name = "物品盘点";

            var listOfItems = _itemRepository.GetAll().Include(x=>x.Positions).ToList();
            //Header 
            template.Cells["E3"].Value = DateTime.Now.ToString("yyyy-MM-dd"); //出单日期               
            //template.Cells["D3"].Value = outstockHeaderViewModel.ApplicationDept; //制表科室
            //template.Cells["F3"].Value = outstockHeaderViewModel.CreatePerson; //制表人
            //template.Cells["H3"].Value = outstockHeaderViewModel.AuditDepart; //审核部门
            //template.Cells["J3"].Value = outstockHeaderViewModel.AuditDepart; //审核人
            //template.Cells["D4"].Value = outstockHeaderViewModel.AuditDepart; //领料人
            //template.Cells["B4"].Value = outstockHeaderViewModel.OutStockNumber; //出库单号
            //template.Cells["F4"].Value = outstockHeaderViewModel.CreatePerson; //领料小组
            //template.Cells["H4"].Value = requestHeader.Contract.Address; //出货地址
            //template.Cells["J4"].Value = requestHeader.Contract.ContractId; //合同编号
            //template.Cells["B5"].Value = requestHeader.RequestCategory.ToString(); //出库类型
                                                                                   //Body
            foreach (var m in listOfItems)
            {
                if (tab)
                {
                    foreach (var p in m.Positions)
                    {
                        var serialNo = "A" + row;
                        var area = "B" + row; //区域
                        var location = "C" + row; //位置
                        var position = "D" + row; //库位
                        var category = "E" + row; //物品类别
                        var projectCategory = "F" + row; //所属专项类
                        var subCategory = "G" + row; //物品种类
                        var code = "H" + row;
                        var chinese = "I" + row;
                        var english = "J" + row;
                        var brand = "L" + row;
                        var model = "M" + row;
                        var specification = "N" + row;
                        var dimension = "O" + row;
                        var lastStockingTotal = "S" + row; //期末数量
                        var unit = "U" + row;
                        var inTotal = "W" + row; //补给数量
                        var note = "X" + row;

                        template.Cells[serialNo].Value = count;
                        template.Cells[area].Value = p.Area;
                        template.Cells[location].Value = p.Location;
                        template.Cells[position].Value = p.PositionName;
                        template.Cells[category].Value = m.Category;
                        template.Cells[projectCategory].Value = m.ProjectCategory;
                        template.Cells[subCategory].Value = m.SubCategory;
                        template.Cells[chinese].Value = m.ChineseName;
                        template.Cells[english].Value = m.EnglishName;
                        template.Cells[brand].Value = m.Brand;
                        template.Cells[code].Value = m.Code;
                        template.Cells[model].Value = m.Model;
                        template.Cells[specification].Value = m.Specification;
                        template.Cells[dimension].Value = m.Dimension;
                        template.Cells[lastStockingTotal].Value = p.Total;
                        template.Cells[inTotal].Value = p.Total;
                        template.Cells[unit].Value = m.Unit;
                        //template.Cells[Diffy].Value = string.Format("{0:C}", model.Price);
                        template.Cells[inTotal].Value = p.Total;
                        template.Cells[note].Value = m.Comments;

                        template.Row(row).Height = 20;
                        template.Cells[serialNo + ":" + note].Style.Font.Size = 10;
                        template.Cells[serialNo + ":" + note].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        template.Cells[serialNo + ":" + note].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        template.Cells[serialNo + ":" + note].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        template.Cells[serialNo + ":" + note].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        template.Cells[serialNo + ":" + note].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        template.Cells[serialNo + ":" + note].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                        row++;
                        count++;
                    }
                }
                else
                {
                    template.Name = "物品盘点2";
                    var serialNo = "A" + row;
                    var category = "E" + row; //物品类别
                    var projectCategory = "F" + row; //所属专项类
                    var subCategory = "G" + row; //物品种类
                    var code = "H" + row;
                    var chinese = "I" + row;
                    var english = "J" + row;
                    var brand = "L" + row;
                    var model = "M" + row;
                    var specification = "N" + row;
                    var dimension = "O" + row;
                    var max = "P" + row; //上限
                    var min = "Q" + row; //下限
                    var lastStockingTotal = "S" + row; //期末数量
                    var unit = "U" + row;
                    var note = "X" + row;

                    template.Cells[serialNo].Value = count;
                    template.Cells[category].Value = m.Category;
                    template.Cells[projectCategory].Value = m.ProjectCategory;
                    template.Cells[subCategory].Value = m.SubCategory;
                    template.Cells[chinese].Value = m.ChineseName;
                    template.Cells[english].Value = m.EnglishName;
                    template.Cells[brand].Value = m.Brand;
                    template.Cells[code].Value = m.Code;
                    template.Cells[model].Value = m.Model;
                    template.Cells[specification].Value = m.Specification;
                    template.Cells[dimension].Value = m.Dimension;
                    template.Cells[lastStockingTotal].Value = m.Positions.Sum(x => x.Total);
                    template.Cells[max].Value = m.Max;
                    template.Cells[min].Value = m.Min;
                    template.Cells[unit].Value = m.Unit;
                    template.Cells[note].Value = m.Comments;

                    template.Row(row).Height = 20;
                    template.Cells[serialNo + ":" + note].Style.Font.Size = 10;
                    template.Cells[serialNo + ":" + note].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    template.Cells[serialNo + ":" + note].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    template.Cells[serialNo + ":" + note].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    template.Cells[serialNo + ":" + note].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    template.Cells[serialNo + ":" + note].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    template.Cells[serialNo + ":" + note].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    row++;
                    count++;
                }

            }
        }

        //上传盘点表
        public int UpdateTotalNumberBySummarySheet(string fileName, out string returnMsg)
        {
            var listOfPositions = _positionRepository.GetAll().ToList();
            returnMsg = string.Empty;
            if (string.IsNullOrEmpty(fileName)) return -2;
            var fi = new FileInfo(fileName);
            var package = new ExcelPackage(fi);
            var workbook = package.Workbook;
            ExcelWorksheet worksheet = workbook.Worksheets[1];
            if (worksheet == null) return 0;
            var end = worksheet.Dimension.End;
            var code= string.Empty;
            var positionName = string.Empty;
            double totalInPut = 0;
            var comment = string.Empty;
            for (var row = 8; row < end.Row; row++)
            {
                for (var col = 2; col <= worksheet.Dimension.End.Column; col++)
                {
                    var readValue = worksheet.Cells[row, col].Value == null ? "" : worksheet.Cells[row, col].Value.ToString();
                    switch (col)
                    {
                        case 4:
                            positionName = readValue;  //位置名称
                            break;
                        case 8:
                            code = readValue; //物品编码
                            break;
                        case 20:
                            totalInPut = readValue == "" ? 0 : double.Parse(readValue); //盘点数据
                            break;
                        case 24:
                            comment = readValue;
                            break;
                    }
                }

                if (string.IsNullOrEmpty(positionName) || string.IsNullOrEmpty(code)) continue;
                var selectPosition = listOfPositions.FirstOrDefault(x => x.PositionName == positionName && x.Code == code);
                if (selectPosition != null)
                {
                    selectPosition.Total = totalInPut;
                    selectPosition.Comment = comment;
                }
            }

            try
            {
                _positionRepository.EditRange(listOfPositions);
                _positionRepository.Save();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

    }
}
