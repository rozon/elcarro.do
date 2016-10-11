namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddYearVehiclePart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleParts", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VehicleParts", "Year");
        }
    }
}
