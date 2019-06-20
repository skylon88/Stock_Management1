using System.Collections.Generic;
using Core.Data;
using Core.Model;
using NewServices.Services;

namespace NewServices
{
    public class DatabaseConfig
    {
        private readonly ManagementService _managementService;
        public DatabaseConfig(ManagementService managementService)
        {
            StockContext context = new StockContext();            
            if (context.Database.Exists())
            {                return;
            }


            context.Database.Create();
            InitData_Generate();
            _managementService = managementService;
            ImportItems();
        }

        public void InitData_Generate()
        {
            using (var ctx = new StockContext())
            {
                var listPriority = new List<Priority>
                {
                    new Priority() {Id = 1, Name = "1级"},
                    new Priority() {Id = 2, Name = "2级"},
                    new Priority() {Id = 3, Name = "3级"}
                };
                ctx.Priorities.AddRange(listPriority);

                ctx.SaveChanges();
            }

        }

        public void ImportItems()
        {
            _managementService.Upload(string.Empty, out _,true);
            _managementService.UploadRelationship(string.Empty,true);
            _managementService.UploadSuppliers(string.Empty,true);
            //_managementService.InitializeSuppliers();
        }
    }
}
