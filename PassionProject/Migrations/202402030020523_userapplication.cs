namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userapplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Applications", "UserId");
            AddForeignKey("dbo.Applications", "UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "UserId", "dbo.Users");
            DropIndex("dbo.Applications", new[] { "UserId" });
            DropColumn("dbo.Applications", "UserId");
        }
    }
}
