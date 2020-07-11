namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0940 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisteredUsers", "Password", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisteredUsers", "Password", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
