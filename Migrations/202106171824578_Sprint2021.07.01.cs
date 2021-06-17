namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210701 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NurserySchoolUserRoles",
                c => new
                    {
                        NurserySchoolUserRoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.NurserySchoolUserRoleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NurserySchoolUserRoles");
        }
    }
}
