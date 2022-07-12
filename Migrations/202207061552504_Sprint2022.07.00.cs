namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220700 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassTimeTables",
                c => new
                    {
                        ClassTimeTableId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ClassTimeTableId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClassTimeTables");
        }
    }
}
