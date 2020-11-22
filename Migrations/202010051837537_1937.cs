namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1937 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "PostTopicId", "dbo.PostTopics");
            DropIndex("dbo.Posts", new[] { "PostTopicId" });
            AlterColumn("dbo.Posts", "PostTopicId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "PostTopicId");
            AddForeignKey("dbo.Posts", "PostTopicId", "dbo.PostTopics", "PostTopicId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PostTopicId", "dbo.PostTopics");
            DropIndex("dbo.Posts", new[] { "PostTopicId" });
            AlterColumn("dbo.Posts", "PostTopicId", c => c.Int());
            CreateIndex("dbo.Posts", "PostTopicId");
            AddForeignKey("dbo.Posts", "PostTopicId", "dbo.PostTopics", "PostTopicId");
        }
    }
}
