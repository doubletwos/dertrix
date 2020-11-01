namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1907 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "OrgGroup_OrgGroupId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "OrgGroup_OrgGroupId");
            AddForeignKey("dbo.RegisteredUsers", "OrgGroup_OrgGroupId", "dbo.OrgGroups", "OrgGroupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "OrgGroup_OrgGroupId", "dbo.OrgGroups");
            DropIndex("dbo.RegisteredUsers", new[] { "OrgGroup_OrgGroupId" });
            DropColumn("dbo.RegisteredUsers", "OrgGroup_OrgGroupId");
        }
    }
}
