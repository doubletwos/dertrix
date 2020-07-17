namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0803 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "CreatedBy");
        }
    }
}
