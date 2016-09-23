namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeProductsCharacters : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCharacters", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductCharacters", "Character_Id", "dbo.Characters");
            DropIndex("dbo.ProductCharacters", new[] { "Product_Id" });
            DropIndex("dbo.ProductCharacters", new[] { "Character_Id" });
            DropTable("dbo.ProductCharacters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductCharacters",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Character_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Character_Id });
            
            CreateIndex("dbo.ProductCharacters", "Character_Id");
            CreateIndex("dbo.ProductCharacters", "Product_Id");
            AddForeignKey("dbo.ProductCharacters", "Character_Id", "dbo.Characters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductCharacters", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
