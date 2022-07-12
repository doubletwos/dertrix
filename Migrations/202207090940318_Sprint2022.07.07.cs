namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220707 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrgClassPeriods",
                c => new
                    {
                        OrgClassPeriodId = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(),
                        ClassRef = c.Int(),
                        OrgId = c.Int(),
                        Period_1 = c.String(),
                        Period_2 = c.String(),
                        Period_3 = c.String(),
                        Period_4 = c.String(),
                        Period_5 = c.String(),
                        Period_6 = c.String(),
                        Period_7 = c.String(),
                        Period_8 = c.String(),
                    })
                .PrimaryKey(t => t.OrgClassPeriodId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrgClassPeriods");
        }
    }
}
