namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1028 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupTypes",
                c => new
                    {
                        GroupTypeId = c.Int(nullable: false, identity: true),
                        GroupTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GroupTypeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GroupTypes");
        }
    }
}
