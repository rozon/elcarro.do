namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMakeAndModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Makes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Make_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Makes", t => t.Make_Id)
                .Index(t => t.Make_Id);
            
            AddColumn("dbo.VehicleParts", "Model_Id", c => c.Int());
            CreateIndex("dbo.VehicleParts", "Model_Id");
            AddForeignKey("dbo.VehicleParts", "Model_Id", "dbo.Models", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleParts", "Model_Id", "dbo.Models");
            DropForeignKey("dbo.Models", "Make_Id", "dbo.Makes");
            DropIndex("dbo.VehicleParts", new[] { "Model_Id" });
            DropIndex("dbo.Models", new[] { "Make_Id" });
            DropColumn("dbo.VehicleParts", "Model_Id");
            DropTable("dbo.Models");
            DropTable("dbo.Makes");
        }
    }
}
