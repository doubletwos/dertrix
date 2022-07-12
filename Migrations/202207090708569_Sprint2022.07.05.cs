namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220705 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId", "dbo.ClassTimeTables");
            DropIndex("dbo.OrgSchDays", new[] { "ClassTimeTable_ClassTimeTableId" });
            AddColumn("dbo.ClassTimeTables", "OrgSchDayId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClassTimeTables", "OrgSchDayId");
            AddForeignKey("dbo.ClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays", "OrgSchDayId", cascadeDelete: false); 
            DropColumn("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId", c => c.Int());
            DropForeignKey("dbo.ClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays");
            DropIndex("dbo.ClassTimeTables", new[] { "OrgSchDayId" });
            DropColumn("dbo.ClassTimeTables", "OrgSchDayId");
            CreateIndex("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId");
            AddForeignKey("dbo.OrgSchDays", "ClassTimeTable_ClassTimeTableId", "dbo.ClassTimeTables", "ClassTimeTableId");
        }
    }
}
