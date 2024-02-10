namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        AppId = c.Int(nullable: false, identity: true),
                        AdoptId = c.Int(nullable: false),
                        PetId = c.Int(nullable: false),
                        AppSubmission = c.DateTime(nullable: false),
                        AppStatus = c.Boolean(nullable: false),
                        AppComments = c.String(),
                    })
                .PrimaryKey(t => t.AppId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Applications");
        }
    }
}
