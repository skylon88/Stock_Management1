namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewunitmodel : DbMigration
    {
        public override void Up()
        {
            //Code-based to migration a new table to existing db
            CreateTable("dbo.UnitModels",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    ItemName = c.String(),
                    DefaultUnit = c.String(),
                    ConvertToUnit = c.String(),
                    Factor = c.Double(),
                    IsGeneral = c.Boolean()
                }).PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
        }
    }
}
