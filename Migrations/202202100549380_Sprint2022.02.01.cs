namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20220201 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Subject_Min_Passmark", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Subject_Min_Passmark");
        }
    }
}
