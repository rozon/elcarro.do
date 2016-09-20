namespace ElCarro.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Suggestion_Updated_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suggestions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        SuggestionMsj = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Suggestions");
        }
    }
}
