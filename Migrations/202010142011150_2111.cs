namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2111 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        JobName = c.String(),
                        JobCreatorId = c.Int(),
                        JobStatus = c.String(),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        OrgId = c.Int(),
                    })
                .PrimaryKey(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Jobs");
        }
    }
}
