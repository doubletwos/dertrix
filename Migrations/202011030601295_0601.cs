namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0601 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guardians",
                c => new
                    {
                        GuardianId = c.Int(nullable: false, identity: true),
                        GuardianFirstName = c.String(),
                        GuardianLastName = c.String(),
                        GuardianFullName = c.String(),
                        GuardianEmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.GuardianId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Guardians");
        }
    }
}
