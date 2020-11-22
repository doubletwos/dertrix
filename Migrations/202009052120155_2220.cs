namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2220 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "OrgBrand_OrgBrandId", "dbo.OrgBrands");
            DropIndex("dbo.Files", new[] { "OrgBrand_OrgBrandId" });
            RenameColumn(table: "dbo.Files", name: "OrgBrand_OrgBrandId", newName: "OrgBrandId");
            AlterColumn("dbo.Files", "OrgBrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Files", "OrgBrandId");
            AddForeignKey("dbo.Files", "OrgBrandId", "dbo.OrgBrands", "OrgBrandId", cascadeDelete: true);
            DropColumn("dbo.Files", "PersonId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "PersonId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Files", "OrgBrandId", "dbo.OrgBrands");
            DropIndex("dbo.Files", new[] { "OrgBrandId" });
            AlterColumn("dbo.Files", "OrgBrandId", c => c.Int());
            RenameColumn(table: "dbo.Files", name: "OrgBrandId", newName: "OrgBrand_OrgBrandId");
            CreateIndex("dbo.Files", "OrgBrand_OrgBrandId");
            AddForeignKey("dbo.Files", "OrgBrand_OrgBrandId", "dbo.OrgBrands", "OrgBrandId");
        }
    }
}
