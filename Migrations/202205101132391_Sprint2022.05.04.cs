namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220504 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupTypes", "Created_date", c => c.DateTime());
            AddColumn("dbo.GroupTypes", "Creator_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupTypes", "Creator_Id");
            DropColumn("dbo.GroupTypes", "Created_date");
        }
    }
}
