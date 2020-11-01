namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1100 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        GroupTypeId = c.Int(),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.GroupTypes", t => t.GroupTypeId)
                .Index(t => t.GroupTypeId);
            
            AddColumn("dbo.Orgs", "GroupId", c => c.Int());
            CreateIndex("dbo.Orgs", "GroupId");
            AddForeignKey("dbo.Orgs", "GroupId", "dbo.Groups", "GroupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orgs", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "GroupTypeId", "dbo.GroupTypes");
            DropIndex("dbo.Groups", new[] { "GroupTypeId" });
            DropIndex("dbo.Orgs", new[] { "GroupId" });
            DropColumn("dbo.Orgs", "GroupId");
            DropTable("dbo.Groups");
        }
    }
}
