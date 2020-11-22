namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1753 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassTeachers",
                c => new
                    {
                        ClassTeacherId = c.Int(nullable: false, identity: true),
                        ClassTeacherName = c.String(),
                    })
                .PrimaryKey(t => t.ClassTeacherId);
            
            AddColumn("dbo.Classes", "ClassTeacherId", c => c.Int());
            CreateIndex("dbo.Classes", "ClassTeacherId");
            AddForeignKey("dbo.Classes", "ClassTeacherId", "dbo.ClassTeachers", "ClassTeacherId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "ClassTeacherId", "dbo.ClassTeachers");
            DropIndex("dbo.Classes", new[] { "ClassTeacherId" });
            DropColumn("dbo.Classes", "ClassTeacherId");
            DropTable("dbo.ClassTeachers");
        }
    }
}
