namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSellPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SellPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SellPrice");
        }
    }
}
