namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan20217 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgImportantDates", "FromImportanttDate", c => c.DateTime());
            AddColumn("dbo.OrgImportantDates", "ToImportanttDate", c => c.DateTime());
            DropColumn("dbo.OrgImportantDates", "ImportanttDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrgImportantDates", "ImportanttDate", c => c.DateTime());
            DropColumn("dbo.OrgImportantDates", "ToImportanttDate");
            DropColumn("dbo.OrgImportantDates", "FromImportanttDate");
        }
    }
}
