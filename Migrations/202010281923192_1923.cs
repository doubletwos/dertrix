namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1923 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegisteredUsers", "OrgGroup_OrgGroupId", "dbo.OrgGroups");
            DropIndex("dbo.RegisteredUsers", new[] { "OrgGroup_OrgGroupId" });
            DropColumn("dbo.RegisteredUsers", "OrgGroup_OrgGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RegisteredUsers", "OrgGroup_OrgGroupId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "OrgGroup_OrgGroupId");
            AddForeignKey("dbo.RegisteredUsers", "OrgGroup_OrgGroupId", "dbo.OrgGroups", "OrgGroupId");
        }
    }
}
