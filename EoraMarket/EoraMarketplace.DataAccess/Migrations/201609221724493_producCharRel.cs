namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producCharRel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Character_Id", "dbo.Characters");
            DropIndex("dbo.Products", new[] { "Character_Id" });
            CreateTable(
                "dbo.ProductCharacters",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Character_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Character_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.Character_Id, cascadeDelete: false)
                .Index(t => t.Product_Id)
                .Index(t => t.Character_Id);
            
            DropColumn("dbo.Products", "Character_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Character_Id", c => c.Int());
            DropForeignKey("dbo.ProductCharacters", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.ProductCharacters", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductCharacters", new[] { "Character_Id" });
            DropIndex("dbo.ProductCharacters", new[] { "Product_Id" });
            DropTable("dbo.ProductCharacters");
            CreateIndex("dbo.Products", "Character_Id");
            AddForeignKey("dbo.Products", "Character_Id", "dbo.Characters", "Id");
        }
    }
}
