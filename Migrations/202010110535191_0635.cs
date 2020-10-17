namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0635 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "ClassTeacherId", c => c.Int());
            CreateIndex("dbo.Subjects", "ClassTeacherId");
            AddForeignKey("dbo.Subjects", "ClassTeacherId", "dbo.ClassTeachers", "ClassTeacherId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "ClassTeacherId", "dbo.ClassTeachers");
            DropIndex("dbo.Subjects", new[] { "ClassTeacherId" });
            DropColumn("dbo.Subjects", "ClassTeacherId");
        }
    }
}
