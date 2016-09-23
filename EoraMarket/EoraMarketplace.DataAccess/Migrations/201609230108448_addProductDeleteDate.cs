namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductDeleteDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MarketProducts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.MarketProducts", "DeletedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MarketProducts", "DeletedAt");
            DropColumn("dbo.MarketProducts", "IsDeleted");
        }
    }
}
