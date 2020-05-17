using System.Data.Entity;
using System.Data.Entity.Migrations;
using Core.Model;

namespace Core.Data
{
  
    public class StockContext : DbContext
    {
        public StockContext() : base("Staging") //Local
        {
            Database.CommandTimeout = 900;
            //Database.SetInitializer(new CreateDatabaseIfNotExists<StockContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StockContext, Migrations.Configuration>());

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

        public DbSet<UnitModel> UnitModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
