namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan20215 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrgImportantDates",
                c => new
                    {
                        OrgImportantDateId = c.Int(nullable: false, identity: true),
                        ImportantDateName = c.String(),
                        OrgId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatorName = c.String(),
                        ImportanttDate = c.DateTime(),
                        DateisPast = c.Boolean(),
                    })
                .PrimaryKey(t => t.OrgImportantDateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrgImportantDates");
        }
    }
}
