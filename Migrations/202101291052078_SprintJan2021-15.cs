namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintJan202115 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGuardians", "Telephone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentGuardians", "Telephone");
        }
    }
}
