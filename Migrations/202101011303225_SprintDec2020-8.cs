namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec20208 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentGuardians", "GuardianId", "dbo.Guardians");
            DropIndex("dbo.StudentGuardians", new[] { "GuardianId" });
            AddColumn("dbo.StudentGuardians", "StudentId", c => c.Int(nullable: false));
            AddColumn("dbo.StudentGuardians", "StudentFullName", c => c.String());
            AddColumn("dbo.StudentGuardians", "OrgId", c => c.Int(nullable: false));
            DropColumn("dbo.StudentGuardians", "GuardianId");
            DropTable("dbo.Guardians");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.StudentGuardians", "GuardianId", c => c.Int(nullable: false));
            DropColumn("dbo.StudentGuardians", "OrgId");
            DropColumn("dbo.StudentGuardians", "StudentFullName");
            DropColumn("dbo.StudentGuardians", "StudentId");
            CreateIndex("dbo.StudentGuardians", "GuardianId");
            AddForeignKey("dbo.StudentGuardians", "GuardianId", "dbo.Guardians", "GuardianId", cascadeDelete: true);
        }
    }
}
