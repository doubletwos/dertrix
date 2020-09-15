namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2254 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "FullName");
        }
    }
}
