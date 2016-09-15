namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Characters", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Characters", "CreatedAt");
        }
    }
}
