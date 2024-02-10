namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "AdoptionId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "AdoptionId");
        }
    }
}
