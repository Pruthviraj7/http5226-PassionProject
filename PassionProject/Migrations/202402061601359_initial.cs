namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adoptions",
                c => new
                    {
                        AdoptionId = c.Int(nullable: false, identity: true),
                        PetId = c.Int(nullable: false),
                        AppId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdoptionId)
                .ForeignKey("dbo.Applications", t => t.AppId, cascadeDelete: true)
                .ForeignKey("dbo.Pets", t => t.PetId, cascadeDelete: true)
                .Index(t => t.PetId)
                .Index(t => t.AppId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adoptions", "PetId", "dbo.Pets");
            DropForeignKey("dbo.Adoptions", "AppId", "dbo.Applications");
            DropIndex("dbo.Adoptions", new[] { "AppId" });
            DropIndex("dbo.Adoptions", new[] { "PetId" });
            DropTable("dbo.Adoptions");
        }
    }
}
