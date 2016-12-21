namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uploadexcelfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleParts", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.VehicleParts", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.VehicleParts", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleParts", "Photo", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleParts", "Description", c => c.String(nullable: false, maxLength: 1000));
            DropColumn("dbo.VehicleParts", "Price");
        }
    }
}
