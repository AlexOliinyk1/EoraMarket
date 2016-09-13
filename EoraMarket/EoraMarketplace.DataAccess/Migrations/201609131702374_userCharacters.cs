namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userCharacters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        Credits = c.Int(nullable: false),
                        Class_Id = c.Int(),
                        Race_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.Class_Id)
                .ForeignKey("dbo.Races", t => t.Race_Id)
                .Index(t => t.Class_Id)
                .Index(t => t.Race_Id);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "Race_Id", "dbo.Races");
            DropForeignKey("dbo.Characters", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Characters", new[] { "Race_Id" });
            DropIndex("dbo.Characters", new[] { "Class_Id" });
            DropTable("dbo.Races");
            DropTable("dbo.Classes");
            DropTable("dbo.Characters");
        }
    }
}
