using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Model;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new StockContext();
            //new DatabaseConfig(ctx).Configure();

           // DataGenerate();
            Bootstrapper.Init();
            Initiator initiator = DependencyInjector.Retrieve<Initiator>();
            //var retrivedRequestInstance = initiator.GetAllData();
            //var retrivedRequestInstance2 = initiator.GetAllHeaderData();
            //var print = DisplayObjectInfo(retrivedRequestInstance.FirstOrDefault());
            //retrivedRequestInstance.FirstOrDefault().Reason = "test23";
            //var newInstance = retrivedRequestInstance.FirstOrDefault();
            //newInstance.Reason = "new instance";
            //var isNew = initiator.AddRequest(newInstance);
            //var isModified = initiator.UpdateRequest(retrivedRequestInstance.FirstOrDefault());

            //var result = ctx.Requests.FirstOrDefault();
            //var purchaseApplicationHeadersResult = ctx.PurchaseApplicationHeaders.FirstOrDefault();
            //var purchaseApplicationsResult = ctx.PurchaseApplications.ToList();
            //var purchase = ctx.Purchases.ToList();
            //var inStock = ctx.InStocks.ToList();

            //Console.WriteLine(print);
            //Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }

        //public static void DataGenerate()
        //{
        //    using (var ctx = new StockContext())
        //    {
        //        var listOfRequestCategories = new List<RequestCategory>();
        //        listOfRequestCategories.Add(new RequestCategory() { Id = 1, Name = "施工材料" });
        //        listOfRequestCategories.Add(new RequestCategory() { Id = 1, Name = "工具借出" });
        //        listOfRequestCategories.Add(new RequestCategory() { Id = 1, Name = "员工补给" });
        //        listOfRequestCategories.Add(new RequestCategory() { Id = 1, Name = "工具维修" });
        //        listOfRequestCategories.Add(new RequestCategory() { Id = 1, Name = "工程车补给" });
        //        listOfRequestCategories.Add(new RequestCategory() { Id = 1, Name = "办公用品" });

        //        var listPriority = new List<Priority>();
        //        listPriority.Add(new Priority() { Id = 1, Name = "1级" });
        //        listPriority.Add(new Priority() { Id = 2, Name = "2级" });
        //        listPriority.Add(new Priority() { Id = 3, Name = "3级" });

        //        var itemInstance = new Item() {
        //            ItemId = Guid.NewGuid(),
        //            Name = "地毯",
        //            Code = "T660033",
        //            Brand = "PureBond",
        //            Model = "Model # H7180-D0220",
        //            Specification = "Chopin Coastal Light 12 ft. x Custom Length Graphic Loop Indoor Carpet",
        //            Dimension = "12尺",
        //            Unit = "平方尺",
        //            Price = 252.51,
        //            Comments = "Test data",
        //            StockNumber = 123,
        //        };
        //        var itemInstance2 = new Item() {
        //            ItemId = Guid.NewGuid(),
        //            Name = "Hinges Closer 门铰自动开关器",
        //            Code = "T660058",
        //            Brand = "PureBond",
        //            Model = "Hinges Closer",
        //            Specification = "17.44-inch W Rectangular LED-Lit Wall Mount Magnifying Mirror in Chrome",
        //            Dimension = "24寸*32寸",
        //            Unit = "套 set",
        //            Price = 469.00,
        //            Comments = "Test data",
        //            StockNumber = 123,
        //        };
        //        var itemInstance3 = new Item()
        //        {
        //            ItemId = Guid.NewGuid(),
        //            Name = "Arborite",
        //            Code = "MIC0001",
        //            Brand = "PureBond",
        //            Model = "Hinges Closer",
        //            Specification = "17.44-inch W Rectangular LED-Lit Wall Mount Magnifying Mirror in Chrome",
        //            Dimension = "24寸*32寸",
        //            Unit = "套 set",
        //            Price = 469.00,
        //            Comments = "Test data",
        //            StockNumber = 123,
        //        };
        //        var itemInstance4 = new Item()
        //        {
        //            ItemId = Guid.NewGuid(),
        //            Name = "橱柜台面 Arborite",
        //            Code = "MIC0004",
        //            Brand = "PureBond",
        //            Model = "Hinges Closer",
        //            Specification = "17.44-inch W Rectangular LED-Lit Wall Mount Magnifying Mirror in Chrome",
        //            Dimension = "24寸*32寸",
        //            Unit = "套 set",
        //            Price = 469.00,
        //            Comments = "Test data",
        //            StockNumber = 123,
        //        };


        //        var poInstance = new POModel() { POId = "PO201810001" };
        //        var contractInstance = new Contract() { ContractId = "C20181028", Address = "123 Finch Ave", POModel = poInstance };
        //        var contractInstance2 = new Contract() { ContractId = "C20181029", Address = "432 Clart Ave", POModel = poInstance };
        //        var requestHeaderInstance = new RequestHeader() { RequestHeaderNumber = "XQ201810001", CreatePerson = "Skylon", Contract = contractInstance };
        //        var requestInstance = new Request()
        //        {
        //            RequestId = Guid.NewGuid(),
        //            Total = 1,
        //            ItemId = itemInstance.ItemId,
        //            RequestNumber = requestHeaderInstance.RequestHeaderNumber
        //        };
        //        var purchaseApplicationHeaderInstance = new PurchaseApplicationHeader()
        //        {
        //            PurchaseApplicationNumber = "SQ201810001",
        //            Status = Core.Enum.ProcessStatus.需求建立,
        //            RequestHeader = requestHeaderInstance
        //        };
        //        var purchaseApplicationInstance1 = new PurchaseApplication()
        //        {
        //            PurchaseApplicationId = Guid.NewGuid(),
        //            TotalApplied = 10,
        //            Item = itemInstance,
        //            PurchaseApplicationHeader = purchaseApplicationHeaderInstance
        //        };
        //        var purchaseApplicationInstance2 = new PurchaseApplication()
        //        {
        //            PurchaseApplicationId = Guid.NewGuid(),
        //            TotalApplied = 8,
        //            Item = itemInstance2,
        //            PurchaseApplicationHeader = purchaseApplicationHeaderInstance
        //        };

        //        var purchaseHeaderInstance = new PurchaseHeader() { PurchaseNumber = "CG201810001" };

        //        var purchaseInstance1 = new Purchase()
        //        {
        //            PurchaseId = Guid.NewGuid(),
        //            PurchaseApplicationId = purchaseApplicationInstance1.PurchaseApplicationId,
        //            PurchaseNumber = purchaseHeaderInstance.PurchaseNumber,
        //            PurchaseTotal = 5
        //        };

        //        var purchaseInstance2 = new Purchase()
        //        {
        //            PurchaseId = Guid.NewGuid(),
        //            PurchaseApplicationId = purchaseApplicationInstance2.PurchaseApplicationId,
        //            PurchaseNumber = purchaseHeaderInstance.PurchaseNumber,
        //            PurchaseTotal = 7
        //        };

        //        var inStockInstance1 = new InStock()
        //        {
        //            InStockNumber = "RK201810001",
        //            PurchaseId = purchaseInstance1.PurchaseId,
        //            RequestId = requestInstance.RequestId,
        //            Item = itemInstance
        //        };

        //        var inStockInstance2 = new InStock()
        //        {
        //            InStockNumber = "RK201810002",
        //            Item = itemInstance2
        //        };


        //        ctx.RequestCategorys.AddRange(listOfRequestCategories);
        //        ctx.Priorities.AddRange(listPriority);

        //        ctx.POModels.Add(poInstance);
        //        ctx.Contracts.Add(contractInstance);
        //        ctx.Contracts.Add(contractInstance2);

        //        ctx.Items.Add(itemInstance);
        //        ctx.Items.Add(itemInstance2);
        //        ctx.Items.Add(itemInstance3);
        //        ctx.Items.Add(itemInstance4);

        //        //ctx.RequestHeaders.Add(requestHeaderInstance);
        //        //ctx.Requests.Add(requestInstance);

        //        //ctx.PurchaseApplicationHeaders.Add(purchaseApplicationHeaderInstance);
        //        //ctx.PurchaseApplications.Add(purchaseApplicationInstance1);
        //        //ctx.PurchaseApplications.Add(purchaseApplicationInstance2);

        //        //ctx.PurchaseHeaders.Add(purchaseHeaderInstance);
        //        //ctx.Purchases.Add(purchaseInstance1);
        //        //ctx.Purchases.Add(purchaseInstance2);

        //        //ctx.InStocks.Add(inStockInstance1);
        //        //ctx.InStocks.Add(inStockInstance2);

        //        ctx.SaveChanges();
        //    }
        //}

        public static string DisplayObjectInfo(Object o)
        {
            StringBuilder sb = new StringBuilder();

            // Include the type of the object
            System.Type type = o.GetType();
            sb.Append("Type: " + type.Name);

            // Include information for each Field
            sb.Append("\r\n\r\nFields:");
            System.Reflection.FieldInfo[] fi = type.GetFields();
            if (fi.Length > 0)
            {
                foreach (FieldInfo f in fi)
                {
                    sb.Append("\r\n " + f.ToString() + " = " + f.GetValue(o));
                }
            }
            else
                sb.Append("\r\n None");

            // Include information for each Property
            sb.Append("\r\n\r\nProperties:");
            System.Reflection.PropertyInfo[] pi = type.GetProperties();
            if (pi.Length > 0)
            {
                foreach (PropertyInfo p in pi)
                {
                    sb.Append("\r\n " + p.ToString() + " = " +
                              p.GetValue(o, null));
                }
            }
            else
                sb.Append("\r\n None");

            return sb.ToString();
        }
    }
}
