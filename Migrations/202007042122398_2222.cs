namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2222 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Religions",
                c => new
                    {
                        ReligionId = c.Int(nullable: false, identity: true),
                        ReligionName = c.String(),
                    })
                .PrimaryKey(t => t.ReligionId);
            
            AddColumn("dbo.RegisteredUsers", "ReligionId", c => c.Int());
            CreateIndex("dbo.RegisteredUsers", "ReligionId");
            AddForeignKey("dbo.RegisteredUsers", "ReligionId", "dbo.Religions", "ReligionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "ReligionId", "dbo.Religions");
            DropIndex("dbo.RegisteredUsers", new[] { "ReligionId" });
            DropColumn("dbo.RegisteredUsers", "ReligionId");
            DropTable("dbo.Religions");
        }
    }
}
