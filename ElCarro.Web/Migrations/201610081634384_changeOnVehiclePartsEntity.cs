namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class changeOnVehiclePartsEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleParts", "Company_Id", "dbo.Companies");
            DropIndex("dbo.VehicleParts", new[] { "Company_Id" });
            AddColumn("dbo.VehicleParts", "Store_StoreID", c => c.Int());
            CreateIndex("dbo.VehicleParts", "Store_StoreID");
            AddForeignKey("dbo.VehicleParts", "Store_StoreID", "dbo.Stores", "StoreID");
            DropColumn("dbo.VehicleParts", "Company_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.VehicleParts", "Company_Id", c => c.Int());
            DropForeignKey("dbo.VehicleParts", "Store_StoreID", "dbo.Stores");
            DropIndex("dbo.VehicleParts", new[] { "Store_StoreID" });
            DropColumn("dbo.VehicleParts", "Store_StoreID");
            CreateIndex("dbo.VehicleParts", "Company_Id");
            AddForeignKey("dbo.VehicleParts", "Company_Id", "dbo.Companies", "Id");
        }
    }
}
