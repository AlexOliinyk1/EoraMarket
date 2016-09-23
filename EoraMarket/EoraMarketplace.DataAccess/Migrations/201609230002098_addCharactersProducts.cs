namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCharactersProducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharactersProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        CharacterId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.CharacterId, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CharacterId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharactersProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CharactersProducts", "CharacterId", "dbo.Characters");
            DropIndex("dbo.CharactersProducts", new[] { "ProductId" });
            DropIndex("dbo.CharactersProducts", new[] { "CharacterId" });
            DropTable("dbo.CharactersProducts");
        }
    }
}
