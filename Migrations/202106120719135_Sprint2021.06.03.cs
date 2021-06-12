namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210603 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGuardians", "Stu_class_Org_Grp_id", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentGuardians", "Stu_class_Org_Grp_id");
        }
    }
}
