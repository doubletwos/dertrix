namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220704 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "ClassTimeTable_ClassTimeTableId", "dbo.ClassTimeTables");
            DropIndex("dbo.Subjects", new[] { "ClassTimeTable_ClassTimeTableId" });
            AddColumn("dbo.ClassTimeTables", "SubjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClassTimeTables", "SubjectId");
            AddForeignKey("dbo.ClassTimeTables", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: false);
            DropColumn("dbo.Subjects", "ClassTimeTable_ClassTimeTableId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "ClassTimeTable_ClassTimeTableId", c => c.Int());
            DropForeignKey("dbo.ClassTimeTables", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.ClassTimeTables", new[] { "SubjectId" });
            DropColumn("dbo.ClassTimeTables", "SubjectId");
            CreateIndex("dbo.Subjects", "ClassTimeTable_ClassTimeTableId");
            AddForeignKey("dbo.Subjects", "ClassTimeTable_ClassTimeTableId", "dbo.ClassTimeTables", "ClassTimeTableId");
        }
    }
}
