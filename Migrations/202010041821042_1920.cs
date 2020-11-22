namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1920 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        PostTopicId = c.Int(),
                        OrgId = c.Int(nullable: false),
                        PostSubject = c.String(),
                        PostCreatorId = c.Int(nullable: false),
                        CreatorFullName = c.String(),
                        PostCreationDate = c.DateTime(),
                        PostExpirtyDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .ForeignKey("dbo.PostTopics", t => t.PostTopicId)
                .Index(t => t.PostTopicId)
                .Index(t => t.OrgId);
            
            CreateTable(
                "dbo.PostTopics",
                c => new
                    {
                        PostTopicId = c.Int(nullable: false, identity: true),
                        PostTopicName = c.String(),
                    })
                .PrimaryKey(t => t.PostTopicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostTopicId", "dbo.PostTopics");
            DropForeignKey("dbo.Posts", "OrgId", "dbo.Orgs");
            DropIndex("dbo.Posts", new[] { "OrgId" });
            DropIndex("dbo.Posts", new[] { "PostTopicId" });
            DropTable("dbo.PostTopics");
            DropTable("dbo.Posts");
        }
    }
}
