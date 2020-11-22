namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2251 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisteredUserTypes", "RegisteredUserTypeName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisteredUserTypes", "RegisteredUserTypeName", c => c.String());
        }
    }
}
