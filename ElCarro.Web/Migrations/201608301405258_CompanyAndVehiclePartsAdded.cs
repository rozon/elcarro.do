namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyAndVehiclePartsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Location = c.Geography(),
                        Admin_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Admin_Id)
                .Index(t => t.Admin_Id);
            
            CreateTable(
                "dbo.VehicleParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Photo = c.String(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleParts", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Companies", "Admin_Id", "dbo.AspNetUsers");
            DropIndex("dbo.VehicleParts", new[] { "Company_Id" });
            DropIndex("dbo.Companies", new[] { "Admin_Id" });
            DropTable("dbo.VehicleParts");
            DropTable("dbo.Companies");
        }
    }
}
