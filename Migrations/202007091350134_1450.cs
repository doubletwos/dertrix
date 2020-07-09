namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1450 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orgs", "OrgTypeId", c => c.Int());
            CreateIndex("dbo.Orgs", "OrgTypeId");
            AddForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes", "OrgTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes");
            DropIndex("dbo.Orgs", new[] { "OrgTypeId" });
            DropColumn("dbo.Orgs", "OrgTypeId");
        }
    }
}
