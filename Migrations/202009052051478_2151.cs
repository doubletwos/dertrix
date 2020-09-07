namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2151 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        OrgBrand_OrgBrandId = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.OrgBrands", t => t.OrgBrand_OrgBrandId)
                .Index(t => t.OrgBrand_OrgBrandId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "OrgBrand_OrgBrandId", "dbo.OrgBrands");
            DropIndex("dbo.Files", new[] { "OrgBrand_OrgBrandId" });
            DropTable("dbo.Files");
        }
    }
}
