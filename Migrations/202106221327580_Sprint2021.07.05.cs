namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210705 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrgSchCalndrGrps",
                c => new
                    {
                        OrgSchCalndrGrpId = c.Int(nullable: false, identity: true),
                        OrgSchCalendarId = c.Int(nullable: false),
                        OrgGroupId = c.Int(),
                        OrgId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgSchCalndrGrpId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrgSchCalndrGrps");
        }
    }
}
