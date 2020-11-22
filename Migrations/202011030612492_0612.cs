namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0612 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentGuardians",
                c => new
                    {
                        StudentGuardianId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        GuardianId = c.Int(nullable: false),
                        GuardianFirstName = c.String(),
                        GuardianLastName = c.String(),
                        GuardianFullName = c.String(),
                        GuardianEmailAddress = c.String(),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.StudentGuardianId)
                .ForeignKey("dbo.Guardians", t => t.GuardianId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.GuardianId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentGuardians", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.StudentGuardians", "GuardianId", "dbo.Guardians");
            DropIndex("dbo.StudentGuardians", new[] { "GuardianId" });
            DropIndex("dbo.StudentGuardians", new[] { "RegisteredUserId" });
            DropTable("dbo.StudentGuardians");
        }
    }
}
