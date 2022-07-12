namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220710 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrgClassTimeTables",
                c => new
                    {
                        OrgClassTimeTableId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.OrgClassTimeTableId);
            
            CreateTable(
                "dbo.OrgSchDays",
                c => new
                    {
                        OrgSchDayId = c.Int(nullable: false, identity: true),
                        DayTypeId = c.Int(nullable: false),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.OrgSchDayId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrgSchDays");
            DropTable("dbo.OrgClassTimeTables");
        }
    }
}
