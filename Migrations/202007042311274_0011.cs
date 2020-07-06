namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0011 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentRegForms",
                c => new
                    {
                        StudentRegFormId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StudentRegFormId);
            
            AddColumn("dbo.RegisteredUsers", "StudentRegFormId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "StudentRegFormId");
            AddForeignKey("dbo.RegisteredUsers", "StudentRegFormId", "dbo.StudentRegForms", "StudentRegFormId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "StudentRegFormId", "dbo.StudentRegForms");
            DropIndex("dbo.RegisteredUsers", new[] { "StudentRegFormId" });
            DropColumn("dbo.RegisteredUsers", "StudentRegFormId");
            DropTable("dbo.StudentRegForms");
        }
    }
}
