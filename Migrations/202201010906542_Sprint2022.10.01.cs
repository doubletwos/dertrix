namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221001 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subjects", "Creator_Id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "Creator_Id", c => c.Int());
        }
    }
}
