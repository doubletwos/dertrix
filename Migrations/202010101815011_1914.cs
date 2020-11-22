namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1914 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                        ClassId = c.Int(nullable: false),
                        RegisteredUsers_RegisteredUserId = c.Int(),
                    })
                .PrimaryKey(t => t.SubjectId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUsers_RegisteredUserId)
                .Index(t => t.ClassId)
                .Index(t => t.RegisteredUsers_RegisteredUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "RegisteredUsers_RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.Subjects", "ClassId", "dbo.Classes");
            DropIndex("dbo.Subjects", new[] { "RegisteredUsers_RegisteredUserId" });
            DropIndex("dbo.Subjects", new[] { "ClassId" });
            DropTable("dbo.Subjects");
        }
    }
}
