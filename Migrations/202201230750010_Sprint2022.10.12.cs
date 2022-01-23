namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sprint20221012 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RemovedRegisteredUsers",
                c => new
                    {
                        RemovedRegisteredUserId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        Telephone = c.String(),
                        RegisteredUserType = c.Int(),
                        PrimarySchoolUserRole = c.Int(),
                        SecondarySchoolUserRole = c.Int(),
                        NurserySchoolUserRole = c.Int(),
                        OrgId = c.Int(),
                        ClassId = c.Int(),
                        ClassRef = c.Int(),
                        GenderId = c.Int(),
                        ReligionId = c.Int(),
                        StudentRegFormId = c.Int(),
                        IsTester = c.Boolean(nullable: false),
                        DateOfBirth = c.DateTime(),
                        EnrolmentDate = c.DateTime(),
                        EnrolledBy = c.Int(),
                    })
                .PrimaryKey(t => t.RemovedRegisteredUserId);
            
            AlterColumn("dbo.Subjects", "First_Term_Test_MaxGrade", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "First_Term_Test_MaxGrade", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.RemovedRegisteredUsers");
        }
    }
}
