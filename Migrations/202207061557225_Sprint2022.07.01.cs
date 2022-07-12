namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220701 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrgSchDays",
                c => new
                    {
                        OrgSchDayId = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.OrgSchDayId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrgSchDays");
        }
    }
}
