namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0816 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "IsTester", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "IsTester");
        }
    }
}
