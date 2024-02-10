namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usermessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "UserId");
            AddForeignKey("dbo.Messages", "UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropColumn("dbo.Messages", "UserId");
        }
    }
}
