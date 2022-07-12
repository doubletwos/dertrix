namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220714 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrgClassTimeTables", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.OrgClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays");
            DropIndex("dbo.OrgClassTimeTables", new[] { "ClassId" });
            DropIndex("dbo.OrgClassTimeTables", new[] { "OrgSchDayId" });
            DropTable("dbo.OrgClassTimeTables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrgClassTimeTables",
                c => new
                    {
                        OrgClassTimeTableId = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(nullable: false),
                        OrgSchDayId = c.Int(nullable: false),
                        OrgId = c.Int(),
                    })
                .PrimaryKey(t => t.OrgClassTimeTableId);
            
            CreateIndex("dbo.OrgClassTimeTables", "OrgSchDayId");
            CreateIndex("dbo.OrgClassTimeTables", "ClassId");
            AddForeignKey("dbo.OrgClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays", "OrgSchDayId", cascadeDelete: true);
            AddForeignKey("dbo.OrgClassTimeTables", "ClassId", "dbo.Classes", "ClassId", cascadeDelete: true);
        }
    }
}
