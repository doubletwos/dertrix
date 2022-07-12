namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220702 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassTimeTables", "ClassId", c => c.Int(nullable: false));
            AddColumn("dbo.ClassTimeTables", "StartTime", c => c.DateTime());
            AddColumn("dbo.ClassTimeTables", "FinishTime", c => c.DateTime());
            AddColumn("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId", c => c.Int());
            AddColumn("dbo.Subjects", "ClassTimeTable_ClassTimeTableId", c => c.Int());
            CreateIndex("dbo.ClassTimeTables", "ClassId");
            CreateIndex("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId");
            CreateIndex("dbo.Subjects", "ClassTimeTable_ClassTimeTableId");
            AddForeignKey("dbo.ClassTimeTables", "ClassId", "dbo.Classes", "ClassId", cascadeDelete: true);
            AddForeignKey("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId", "dbo.ClassTimeTables", "ClassTimeTableId");
            AddForeignKey("dbo.Subjects", "ClassTimeTable_ClassTimeTableId", "dbo.ClassTimeTables", "ClassTimeTableId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "ClassTimeTable_ClassTimeTableId", "dbo.ClassTimeTables");
            DropForeignKey("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId", "dbo.ClassTimeTables");
            DropForeignKey("dbo.ClassTimeTables", "ClassId", "dbo.Classes");
            DropIndex("dbo.Subjects", new[] { "ClassTimeTable_ClassTimeTableId" });
            DropIndex("dbo.OrgSchDays", new[] { "ClassTimeTable_ClassTimeTableId" });
            DropIndex("dbo.ClassTimeTables", new[] { "ClassId" });
            DropColumn("dbo.Subjects", "ClassTimeTable_ClassTimeTableId");
            DropColumn("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId");
            DropColumn("dbo.ClassTimeTables", "FinishTime");
            DropColumn("dbo.ClassTimeTables", "StartTime");
            DropColumn("dbo.ClassTimeTables", "ClassId");
        }
    }
}
