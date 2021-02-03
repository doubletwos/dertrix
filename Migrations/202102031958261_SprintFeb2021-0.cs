namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SprintFeb20210 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Students_Count", c => c.Int());
            AddColumn("dbo.Classes", "Female_Students_Count", c => c.Int());
            AddColumn("dbo.Classes", "Male_Students_Count", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "Male_Students_Count");
            DropColumn("dbo.Classes", "Female_Students_Count");
            DropColumn("dbo.Classes", "Students_Count");
        }
    }
}
