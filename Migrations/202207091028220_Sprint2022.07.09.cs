namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220709 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrgClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays");
            DropIndex("dbo.OrgClassTimeTables", new[] { "OrgSchDayId" });
            DropTable("dbo.OrgClassTimeTables");
            DropTable("dbo.OrgSchDays");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrgSchDays",
                c => new
                    {
                        OrgSchDayId = c.Int(nullable: false, identity: true),
                        DayTypeId = c.Int(nullable: false),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.OrgSchDayId);
            
            CreateTable(
                "dbo.OrgClassTimeTables",
                c => new
                    {
                        OrgClassTimeTableId = c.Int(nullable: false, identity: true),
                        OrgSchDayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgClassTimeTableId);
            
            CreateIndex("dbo.OrgClassTimeTables", "OrgSchDayId");
            AddForeignKey("dbo.OrgClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays", "OrgSchDayId", cascadeDelete: true);
        }
    }
}
