namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2138 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        GenderName = c.String(),
                    })
                .PrimaryKey(t => t.GenderId);
            
            CreateTable(
                "dbo.Tribes",
                c => new
                    {
                        TribeId = c.Int(nullable: false, identity: true),
                        TribeName = c.String(),
                    })
                .PrimaryKey(t => t.TribeId);
            
            AddColumn("dbo.RegisteredUsers", "GenderId", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "TribeId", c => c.Int());
            AddColumn("dbo.RegisteredUsers", "DateOfBirth", c => c.DateTime());
            AddColumn("dbo.RegisteredUsers", "EnrolmentDate", c => c.DateTime());
            CreateIndex("dbo.RegisteredUsers", "GenderId");
            CreateIndex("dbo.RegisteredUsers", "TribeId");
            AddForeignKey("dbo.RegisteredUsers", "GenderId", "dbo.Genders", "GenderId");
            AddForeignKey("dbo.RegisteredUsers", "TribeId", "dbo.Tribes", "TribeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredUsers", "TribeId", "dbo.Tribes");
            DropForeignKey("dbo.RegisteredUsers", "GenderId", "dbo.Genders");
            DropIndex("dbo.RegisteredUsers", new[] { "TribeId" });
            DropIndex("dbo.RegisteredUsers", new[] { "GenderId" });
            DropColumn("dbo.RegisteredUsers", "EnrolmentDate");
            DropColumn("dbo.RegisteredUsers", "DateOfBirth");
            DropColumn("dbo.RegisteredUsers", "TribeId");
            DropColumn("dbo.RegisteredUsers", "GenderId");
            DropTable("dbo.Tribes");
            DropTable("dbo.Genders");
        }
    }
}
