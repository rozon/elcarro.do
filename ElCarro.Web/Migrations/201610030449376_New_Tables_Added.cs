namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_Tables_Added : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleParts", "Company_Id", "dbo.Companies");
            DropIndex("dbo.VehicleParts", new[] { "Company_Id" });
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 75),
                        Logo = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoreID)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Review = c.String(nullable: false, maxLength: 255),
                        StoreId = c.Int(nullable: false),
                        PuntuationId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Puntuations", t => t.PuntuationId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.StoreId)
                .Index(t => t.PuntuationId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Puntuations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Level = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StoreAddress",
                c => new
                    {
                        StoreID = c.Int(nullable: false),
                        Zone = c.String(nullable: false, maxLength: 25),
                        Province = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        StreetName = c.String(nullable: false, maxLength: 75),
                        StreetNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoreID)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .Index(t => t.StoreID);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VehicleParts", t => t.PartId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicle", t => t.VehiculeId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.VehiculeId)
                .Index(t => t.PartId);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false, maxLength: 30),
                        Model = c.String(nullable: false, maxLength: 50),
                        Year = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.VehicleParts", "Name", c => c.String(nullable: false, maxLength: 75));
            AddColumn("dbo.VehicleParts", "Last_View", c => c.DateTime(nullable: false));
            AddColumn("dbo.VehicleParts", "Popularity", c => c.Int(nullable: false));
            AlterColumn("dbo.Companies", "Name", c => c.String(nullable: false, maxLength: 75));
            AlterColumn("dbo.VehicleParts", "Description", c => c.String(maxLength: 1000));
            DropColumn("dbo.Companies", "Address");
            DropColumn("dbo.VehicleParts", "Company_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VehicleParts", "Company_Id", c => c.Int());
            AddColumn("dbo.Companies", "Address", c => c.String());
            DropForeignKey("dbo.Store_Items", "VehiculeId", "dbo.Vehicle");
            DropForeignKey("dbo.Store_Items", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Store_Items", "PartId", "dbo.VehicleParts");
            DropForeignKey("dbo.StoreAddress", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Reviews", "PuntuationId", "dbo.Puntuations");
            DropForeignKey("dbo.Stores", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Store_Items", new[] { "PartId" });
            DropIndex("dbo.Store_Items", new[] { "VehiculeId" });
            DropIndex("dbo.Store_Items", new[] { "StoreId" });
            DropIndex("dbo.StoreAddress", new[] { "StoreID" });
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "PuntuationId" });
            DropIndex("dbo.Reviews", new[] { "StoreId" });
            DropIndex("dbo.Stores", new[] { "CompanyId" });
            AlterColumn("dbo.VehicleParts", "Description", c => c.String());
            AlterColumn("dbo.Companies", "Name", c => c.String());
            DropColumn("dbo.VehicleParts", "Popularity");
            DropColumn("dbo.VehicleParts", "Last_View");
            DropColumn("dbo.VehicleParts", "Name");
            DropTable("dbo.Vehicle");
            DropTable("dbo.Store_Items");
            DropTable("dbo.StoreAddress");
            DropTable("dbo.Puntuations");
            DropTable("dbo.Reviews");
            DropTable("dbo.Stores");
            CreateIndex("dbo.VehicleParts", "Company_Id");
            AddForeignKey("dbo.VehicleParts", "Company_Id", "dbo.Companies", "Id");
        }
    }
}
