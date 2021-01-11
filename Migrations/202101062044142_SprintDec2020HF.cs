namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintDec2020HF : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        RelationshipId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RelationshipId);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        TitleId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.TitleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Titles");
            DropTable("dbo.Relationships");
        }
    }
}
