namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChanginVehiclePart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Store_Items", "PartId", "dbo.VehicleParts");
            DropForeignKey("dbo.Store_Items", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Store_Items", "VehiculeId", "dbo.Vehicle");
            DropForeignKey("dbo.VehicleParts", "Model_Id", "dbo.Models");
            DropForeignKey("dbo.VehicleParts", "Store_StoreID", "dbo.Stores");
            DropIndex("dbo.Store_Items", new[] { "StoreId" });
            DropIndex("dbo.Store_Items", new[] { "VehiculeId" });
            DropIndex("dbo.Store_Items", new[] { "PartId" });
            DropIndex("dbo.VehicleParts", new[] { "Model_Id" });
            DropIndex("dbo.VehicleParts", new[] { "Store_StoreID" });
            AlterColumn("dbo.VehicleParts", "Description", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.VehicleParts", "Photo", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleParts", "Model_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.VehicleParts", "Store_StoreID", c => c.Int(nullable: false));
            CreateIndex("dbo.VehicleParts", "Model_Id");
            CreateIndex("dbo.VehicleParts", "Store_StoreID");
            AddForeignKey("dbo.VehicleParts", "Model_Id", "dbo.Models", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VehicleParts", "Store_StoreID", "dbo.Stores", "StoreID", cascadeDelete: true);
            DropTable("dbo.Store_Items");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Store_Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Available = c.Boolean(nullable: false),
                        StoreId = c.Int(nullable: false),
                        VehiculeId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.VehicleParts", "Store_StoreID", "dbo.Stores");
            DropForeignKey("dbo.VehicleParts", "Model_Id", "dbo.Models");
            DropIndex("dbo.VehicleParts", new[] { "Store_StoreID" });
            DropIndex("dbo.VehicleParts", new[] { "Model_Id" });
            AlterColumn("dbo.VehicleParts", "Store_StoreID", c => c.Int());
            AlterColumn("dbo.VehicleParts", "Model_Id", c => c.Int());
            AlterColumn("dbo.VehicleParts", "Photo", c => c.String());
            AlterColumn("dbo.VehicleParts", "Description", c => c.String(maxLength: 1000));
            CreateIndex("dbo.VehicleParts", "Store_StoreID");
            CreateIndex("dbo.VehicleParts", "Model_Id");
            CreateIndex("dbo.Store_Items", "PartId");
            CreateIndex("dbo.Store_Items", "VehiculeId");
            CreateIndex("dbo.Store_Items", "StoreId");
            AddForeignKey("dbo.VehicleParts", "Store_StoreID", "dbo.Stores", "StoreID");
            AddForeignKey("dbo.VehicleParts", "Model_Id", "dbo.Models", "Id");
            AddForeignKey("dbo.Store_Items", "VehiculeId", "dbo.Vehicle", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Store_Items", "StoreId", "dbo.Stores", "StoreID", cascadeDelete: true);
            AddForeignKey("dbo.Store_Items", "PartId", "dbo.VehicleParts", "Id", cascadeDelete: true);
        }
    }
}
