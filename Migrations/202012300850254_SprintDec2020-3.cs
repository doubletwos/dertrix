namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec20203 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrgGroups", "Post_PostId", c => c.Int());
            AlterColumn("dbo.Orgs", "OrgName", c => c.String());
            AlterColumn("dbo.Orgs", "OrgAddress", c => c.String());
            CreateIndex("dbo.OrgGroups", "Post_PostId");
            AddForeignKey("dbo.OrgGroups", "Post_PostId", "dbo.Posts", "PostId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrgGroups", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.OrgGroups", new[] { "Post_PostId" });
            AlterColumn("dbo.Orgs", "OrgAddress", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Orgs", "OrgName", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.OrgGroups", "Post_PostId");
        }
    }
}
