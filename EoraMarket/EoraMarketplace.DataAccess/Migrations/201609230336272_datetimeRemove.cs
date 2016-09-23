namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeRemove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Characters", "CreatedAt");
            DropColumn("dbo.CharactersProducts", "CreatedAt");
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.MarketProducts", "DeletedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MarketProducts", "DeletedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.CharactersProducts", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Characters", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
