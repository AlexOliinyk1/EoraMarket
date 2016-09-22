namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeRelProdClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "Product_Id", "dbo.Products");
            DropIndex("dbo.Classes", new[] { "Product_Id" });
            CreateTable(
                "dbo.ProductClasses",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Class_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Class_Id);
            
            DropColumn("dbo.Classes", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classes", "Product_Id", c => c.Int());
            DropForeignKey("dbo.ProductClasses", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.ProductClasses", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductClasses", new[] { "Class_Id" });
            DropIndex("dbo.ProductClasses", new[] { "Product_Id" });
            DropTable("dbo.ProductClasses");
            CreateIndex("dbo.Classes", "Product_Id");
            AddForeignKey("dbo.Classes", "Product_Id", "dbo.Products", "Id");
        }
    }
}
