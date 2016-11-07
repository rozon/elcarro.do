namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingAddressofStore : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StoreAddress", "StoreID", "dbo.Stores");
            DropIndex("dbo.StoreAddress", new[] { "StoreID" });
            DropTable("dbo.StoreAddress");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.StoreID);
            
            CreateIndex("dbo.StoreAddress", "StoreID");
            AddForeignKey("dbo.StoreAddress", "StoreID", "dbo.Stores", "StoreID");
        }
    }
}
