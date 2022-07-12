namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220708 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays");
            DropIndex("dbo.ClassTimeTables", new[] { "OrgSchDayId" });
            CreateTable(
                "dbo.OrgClassTimeTables",
                c => new
                    {
                        OrgClassTimeTableId = c.Int(nullable: false, identity: true),
                        OrgSchDayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgClassTimeTableId)
                .ForeignKey("dbo.OrgSchDays", t => t.OrgSchDayId, cascadeDelete: false)
                .Index(t => t.OrgSchDayId);
            
            DropTable("dbo.ClassTimeTables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClassTimeTables",
                c => new
                    {
                        ClassTimeTableId = c.Int(nullable: false, identity: true),
                        OrgSchDayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassTimeTableId);
            
            DropForeignKey("dbo.OrgClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays");
            DropIndex("dbo.OrgClassTimeTables", new[] { "OrgSchDayId" });
            DropTable("dbo.OrgClassTimeTables");
            CreateIndex("dbo.ClassTimeTables", "OrgSchDayId");
            AddForeignKey("dbo.ClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays", "OrgSchDayId", cascadeDelete: true);
        }
    }
}
