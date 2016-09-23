namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CharactersProducts", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.MarketProducts", "DeletedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "LockoutEndDateUtc", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LockoutEndDateUtc", c => c.DateTime());
            DropColumn("dbo.MarketProducts", "DeletedAt");
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.CharactersProducts", "CreatedAt");
            DropColumn("dbo.Characters", "CreatedAt");
        }
    }
}
