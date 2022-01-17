namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221007 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrgSchPostGrps",
                c => new
                    {
                        OrgSchPostGrpId = c.Int(nullable: false, identity: true),
                        OrgPostId = c.Int(nullable: false),
                        OrgGroupId = c.Int(),
                        OrgId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgSchPostGrpId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrgSchPostGrps");
        }
    }
}
