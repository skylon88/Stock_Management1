using System.Data.Entity;
using System.Data.Entity.Migrations;
using Core.Model;

namespace Core.Data
{
  
    public class StockContext : DbContext
    {
        //"stock_management_2019-08-25T02-00Z"
        //"Stock_Management"
        //"Data Source=componentelement-db.database.windows.net;Initial Catalog=stock_management;User ID=reportAdmin;Password=@W19880413;Integrated Security=False;Connect Timeout=600";
        public StockContext() : base("Data Source=componentelement-db.database.windows.net;Initial Catalog=stock_management_2019-08-25T02-00Z;User ID=reportAdmin;Password=@W19880413;Integrated Security=False;Connect Timeout=600")
        {
            Database.CommandTimeout = 900;
            Database.SetInitializer(new CreateDatabaseIfNotExists<StockContext>());

        }
        public DbSet<PoModel> PoModels { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestHeader> RequestHeaders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OutStockHeader> OutStockHeaders { get; set; }
        public DbSet<OutStock> OutStocks { get; set; }

        public DbSet<PurchaseApplicationHeader> PurchaseApplicationHeaders { get; set; }
        public DbSet<PurchaseApplication> PurchaseApplications { get; set; }

        public DbSet<PurchaseHeader> PurchaseHeaders { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<InStockHeader> InStockHeaders { get; set; }
        public DbSet<InStock> InStocks { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<FixModel> FixModels { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
    }

    public class MyContextConfiguration : DbMigrationsConfiguration<StockContext> {
        public MyContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            CommandTimeout = 900;
        }
    }



}
