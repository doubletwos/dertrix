namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220706 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassTimeTables", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.ClassTimeTables", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.ClassTimeTables", new[] { "ClassId" });
            DropIndex("dbo.ClassTimeTables", new[] { "SubjectId" });
            DropColumn("dbo.ClassTimeTables", "ClassId");
            DropColumn("dbo.ClassTimeTables", "SubjectId");
            DropColumn("dbo.ClassTimeTables", "StartTime");
            DropColumn("dbo.ClassTimeTables", "FinishTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassTimeTables", "FinishTime", c => c.DateTime());
            AddColumn("dbo.ClassTimeTables", "StartTime", c => c.DateTime());
            AddColumn("dbo.ClassTimeTables", "SubjectId", c => c.Int(nullable: false));
            AddColumn("dbo.ClassTimeTables", "ClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClassTimeTables", "SubjectId");
            CreateIndex("dbo.ClassTimeTables", "ClassId");
            AddForeignKey("dbo.ClassTimeTables", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: true);
            AddForeignKey("dbo.ClassTimeTables", "ClassId", "dbo.Classes", "ClassId", cascadeDelete: true);
        }
    }
}
