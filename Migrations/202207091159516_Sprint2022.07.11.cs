namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220711 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgClassTimeTables", "ClassId", c => c.Int(nullable: false));
            AddColumn("dbo.OrgClassTimeTables", "OrgSchDayId", c => c.Int(nullable: false));
            AddColumn("dbo.OrgClassTimeTables", "OrgId", c => c.Int());
            CreateIndex("dbo.OrgClassTimeTables", "ClassId");
            CreateIndex("dbo.OrgClassTimeTables", "OrgSchDayId");
            AddForeignKey("dbo.OrgClassTimeTables", "ClassId", "dbo.Classes", "ClassId", cascadeDelete: false);
            AddForeignKey("dbo.OrgClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays", "OrgSchDayId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrgClassTimeTables", "OrgSchDayId", "dbo.OrgSchDays");
            DropForeignKey("dbo.OrgClassTimeTables", "ClassId", "dbo.Classes");
            DropIndex("dbo.OrgClassTimeTables", new[] { "OrgSchDayId" });
            DropIndex("dbo.OrgClassTimeTables", new[] { "ClassId" });
            DropColumn("dbo.OrgClassTimeTables", "OrgId");
            DropColumn("dbo.OrgClassTimeTables", "OrgSchDayId");
            DropColumn("dbo.OrgClassTimeTables", "ClassId");
        }
    }
}
