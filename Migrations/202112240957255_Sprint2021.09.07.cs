namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20210907 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Created_date", c => c.DateTime());
            AddColumn("dbo.Subjects", "Creator_Id", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Creator_Id");
            DropColumn("dbo.Subjects", "Created_date");
        }
    }
}
