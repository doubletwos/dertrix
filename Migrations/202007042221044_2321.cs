namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2321 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUsers", "RegisteredUserRoleID", "dbo.RegisteredUserRoles");
            DropIndex("dbo.RegisteredUsers", new[] { "RegisteredUserRoleID" });
            CreateTable(
                "dbo.PrimarySchoolUserRoles",
                c => new
                    {
                        PrimarySchoolUserRoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.PrimarySchoolUserRoleID);
            
            CreateTable(
                "dbo.SecondarySchoolUserRoles",
                c => new
                    {
                        SecondarySchoolUserRoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.SecondarySchoolUserRoleId);
            
            AddColumn("dbo.RegisteredUsers", "PrimarySchoolUserRoleId", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "SecondarySchoolUserRoleId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "PrimarySchoolUserRoleId");
            CreateIndex("dbo.RegisteredUsers", "SecondarySchoolUserRoleId");
            AddForeignKey("dbo.RegisteredUsers", "PrimarySchoolUserRoleId", "dbo.PrimarySchoolUserRoles", "PrimarySchoolUserRoleID");
            AddForeignKey("dbo.RegisteredUsers", "SecondarySchoolUserRoleId", "dbo.SecondarySchoolUserRoles", "SecondarySchoolUserRoleId");
            DropColumn("dbo.RegisteredUsers", "RegisteredUserRoleID");
            DropTable("dbo.RegisteredUserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RegisteredUserRoles",
                c => new
                    {
                        RegisteredUserRoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RegisteredUserRoleID);
            
            AddColumn("dbo.RegisteredUsers", "RegisteredUserRoleID", c => c.Int());
            DropForeignKey("dbo.RegisteredUsers", "SecondarySchoolUserRoleId", "dbo.SecondarySchoolUserRoles");
            DropForeignKey("dbo.RegisteredUsers", "PrimarySchoolUserRoleId", "dbo.PrimarySchoolUserRoles");
            DropIndex("dbo.RegisteredUsers", new[] { "SecondarySchoolUserRoleId" });
            DropIndex("dbo.RegisteredUsers", new[] { "PrimarySchoolUserRoleId" });
            DropColumn("dbo.RegisteredUsers", "SecondarySchoolUserRoleId");
            DropColumn("dbo.RegisteredUsers", "PrimarySchoolUserRoleId");
            DropTable("dbo.SecondarySchoolUserRoles");
            DropTable("dbo.PrimarySchoolUserRoles");
            CreateIndex("dbo.RegisteredUsers", "RegisteredUserRoleID");
            AddForeignKey("dbo.RegisteredUsers", "RegisteredUserRoleID", "dbo.RegisteredUserRoles", "RegisteredUserRoleID");
        }
    }
}
