namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1422 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassTypes",
                c => new
                    {
                        ClassTypeId = c.Int(nullable: false, identity: true),
                        ClassTypeName = c.String(),
                        Class_ClassId = c.Int(),
                    })
                .PrimaryKey(t => t.ClassTypeId)
                .ForeignKey("dbo.Classes", t => t.Class_ClassId)
                .Index(t => t.Class_ClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassTypes", "Class_ClassId", "dbo.Classes");
            DropIndex("dbo.ClassTypes", new[] { "Class_ClassId" });
            DropTable("dbo.ClassTypes");
        }
    }
}
