namespace EoraMarketplace.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refCharToUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Characters", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Characters", "Race_Id", "dbo.Races");
            DropIndex("dbo.Characters", new[] { "Class_Id" });
            DropIndex("dbo.Characters", new[] { "Race_Id" });
            RenameColumn(table: "dbo.Characters", name: "Class_Id", newName: "ClassId");
            RenameColumn(table: "dbo.Characters", name: "Race_Id", newName: "RaceId");
            CreateTable(
                "dbo.AvatarImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Race_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Races", t => t.Race_Id)
                .Index(t => t.Race_Id);
            
            AddColumn("dbo.Characters", "OwnerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Characters", "ClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Characters", "RaceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Characters", "RaceId");
            CreateIndex("dbo.Characters", "ClassId");
            CreateIndex("dbo.Characters", "OwnerId");
            AddForeignKey("dbo.Characters", "OwnerId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Characters", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Characters", "RaceId", "dbo.Races", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "RaceId", "dbo.Races");
            DropForeignKey("dbo.Characters", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.AvatarImages", "Race_Id", "dbo.Races");
            DropForeignKey("dbo.Characters", "OwnerId", "dbo.Users");
            DropIndex("dbo.Characters", new[] { "OwnerId" });
            DropIndex("dbo.Characters", new[] { "ClassId" });
            DropIndex("dbo.Characters", new[] { "RaceId" });
            DropIndex("dbo.AvatarImages", new[] { "Race_Id" });
            AlterColumn("dbo.Characters", "RaceId", c => c.Int());
            AlterColumn("dbo.Characters", "ClassId", c => c.Int());
            DropColumn("dbo.Characters", "OwnerId");
            DropTable("dbo.AvatarImages");
            RenameColumn(table: "dbo.Characters", name: "RaceId", newName: "Race_Id");
            RenameColumn(table: "dbo.Characters", name: "ClassId", newName: "Class_Id");
            CreateIndex("dbo.Characters", "Race_Id");
            CreateIndex("dbo.Characters", "Class_Id");
            AddForeignKey("dbo.Characters", "Race_Id", "dbo.Races", "Id");
            AddForeignKey("dbo.Characters", "Class_Id", "dbo.Classes", "Id");
        }
    }
}
