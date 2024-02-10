namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        PetId = c.Int(nullable: false, identity: true),
                        PetName = c.String(),
                        PetAge = c.Int(nullable: false),
                        PetSpecies = c.String(),
                        PetBreed = c.String(),
                        PetAdoptionStatus = c.Boolean(nullable: false),
                        PetDescription = c.String(),
                    })
                .PrimaryKey(t => t.PetId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pets");
        }
    }
}
