namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productsMarket : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AvatarImages", newName: "Pictures");
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Character_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.ImageId, cascadeDelete: true)
                .ForeignKey("dbo.ProductTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.Character_Id)
                .Index(t => t.ImageId)
                .Index(t => t.TypeId)
                .Index(t => t.Character_Id);
            
            CreateTable(
                "dbo.ProductStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatName = c.String(),
                        StatValue = c.Int(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarketProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Characters", "ImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Classes", "Product_Id", c => c.Int());
            AddColumn("dbo.Pictures", "IsAvatar", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Characters", "ImageId");
            CreateIndex("dbo.Classes", "Product_Id");
            AddForeignKey("dbo.Characters", "ImageId", "dbo.Pictures", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Classes", "Product_Id", "dbo.Products", "Id");
            DropColumn("dbo.Characters", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Characters", "ImageUrl", c => c.String());
            DropForeignKey("dbo.MarketProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "Character_Id", "dbo.Characters");
            DropForeignKey("dbo.Products", "TypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductStats", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "ImageId", "dbo.Pictures");
            DropForeignKey("dbo.Classes", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Characters", "ImageId", "dbo.Pictures");
            DropIndex("dbo.MarketProducts", new[] { "ProductId" });
            DropIndex("dbo.ProductStats", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "Character_Id" });
            DropIndex("dbo.Products", new[] { "TypeId" });
            DropIndex("dbo.Products", new[] { "ImageId" });
            DropIndex("dbo.Classes", new[] { "Product_Id" });
            DropIndex("dbo.Characters", new[] { "ImageId" });
            DropColumn("dbo.Pictures", "IsAvatar");
            DropColumn("dbo.Classes", "Product_Id");
            DropColumn("dbo.Characters", "ImageId");
            DropTable("dbo.MarketProducts");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.ProductStats");
            DropTable("dbo.Products");
            RenameTable(name: "dbo.Pictures", newName: "AvatarImages");
        }
    }
}
