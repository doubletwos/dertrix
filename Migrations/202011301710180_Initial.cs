namespace Dertrix.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {

          

            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                        ClassRefNumb = c.Int(),
                        ClassIsActive = c.Boolean(nullable: false),
                        OrgId = c.Int(nullable: false),
                        ClassTeacherId = c.Int(),
                        ClassTeacherFullName = c.String(),
                    })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("dbo.ClassTeachers", t => t.ClassTeacherId)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .Index(t => t.OrgId)
                .Index(t => t.ClassTeacherId);
            
            CreateTable(
                "dbo.ClassTeachers",
                c => new
                    {
                        ClassTeacherId = c.Int(nullable: false, identity: true),
                        ClassTeacherName = c.String(),
                    })
                .PrimaryKey(t => t.ClassTeacherId);
            
            CreateTable(
                "dbo.ClassTypes",
                c => new
                    {
                        ClassTypeId = c.Int(nullable: false, identity: true),
                        ClassTypeName = c.String(),
                        Class_ClassId = c.Int(),
                    })
                .PrimaryKey(t => t.ClassTypeId)
                .ForeignKey("dbo.Classes", t => t.Class_ClassId)
                .Index(t => t.Class_ClassId);
            
            CreateTable(
                "dbo.Orgs",
                c => new
                    {
                        OrgId = c.Int(nullable: false, identity: true),
                        OrgName = c.String(nullable: false, maxLength: 30),
                        OrgAddress = c.String(nullable: false, maxLength: 30),
                        CreationDate = c.DateTime(),
                        DomainId = c.Int(nullable: false),
                        OrgBrandId = c.Int(nullable: false),
                        OrgTypeId = c.Int(),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.OrgId)
                .ForeignKey("dbo.Domains", t => t.DomainId, cascadeDelete: true)
                .ForeignKey("dbo.OrgBrands", t => t.OrgBrandId, cascadeDelete: true)
                .ForeignKey("dbo.OrgTypes", t => t.OrgTypeId)
                .Index(t => t.DomainId)
                .Index(t => t.OrgBrandId)
                .Index(t => t.OrgTypeId);
            
            CreateTable(
                "dbo.Domains",
                c => new
                    {
                        DomainId = c.Int(nullable: false, identity: true),
                        DomainName = c.String(),
                    })
                .PrimaryKey(t => t.DomainId);
            
            CreateTable(
                "dbo.OrgBrands",
                c => new
                    {
                        OrgBrandId = c.Int(nullable: false, identity: true),
                        OrgBrandName = c.String(),
                        OrgBrandBar = c.String(),
                        OrgNavigationBar = c.String(),
                        OrgNavBarTextColour = c.String(),
                        OrgBrandButtonColour = c.String(),
                    })
                .PrimaryKey(t => t.OrgBrandId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        OrgBrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.OrgBrands", t => t.OrgBrandId, cascadeDelete: true)
                .Index(t => t.OrgBrandId);
            
            CreateTable(
                "dbo.OrgTypes",
                c => new
                    {
                        OrgTypeId = c.Int(nullable: false, identity: true),
                        OrgTypeName = c.String(),
                    })
                .PrimaryKey(t => t.OrgTypeId);
            
            CreateTable(
                "dbo.RegisteredUsers",
                c => new
                    {
                        RegisteredUserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 12),
                        LastName = c.String(nullable: false, maxLength: 12),
                        FullName = c.String(),
                        Email = c.String(nullable: false),
                        LoginErrorMsg = c.String(),
                        Password = c.String(maxLength: 100),
                        ConfirmPassword = c.String(),
                        Telephone = c.String(),
                        RegisteredUserTypeId = c.Int(nullable: false),
                        PrimarySchoolUserRoleId = c.Int(),
                        SecondarySchoolUserRoleId = c.Int(),
                        SelectedOrg = c.Int(nullable: false),
                        ClassId = c.Int(),
                        GenderId = c.Int(),
                        ReligionId = c.Int(),
                        StudentRegFormId = c.Int(),
                        IsTester = c.Boolean(),
                        TribeId = c.Int(),
                        DateOfBirth = c.DateTime(),
                        EnrolmentDate = c.DateTime(),
                        CreatedBy = c.String(),
                        RegUserOrgBrand = c.Int(),
                        Org_OrgId = c.Int(),
                    })
                .PrimaryKey(t => t.RegisteredUserId)
                .ForeignKey("dbo.Classes", t => t.ClassId)
                .ForeignKey("dbo.Genders", t => t.GenderId)
                .ForeignKey("dbo.Orgs", t => t.Org_OrgId)
                .ForeignKey("dbo.PrimarySchoolUserRoles", t => t.PrimarySchoolUserRoleId)
                .ForeignKey("dbo.RegisteredUserTypes", t => t.RegisteredUserTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Religions", t => t.ReligionId)
                .ForeignKey("dbo.SecondarySchoolUserRoles", t => t.SecondarySchoolUserRoleId)
                .ForeignKey("dbo.StudentRegForms", t => t.StudentRegFormId)
                .ForeignKey("dbo.Tribes", t => t.TribeId)
                .Index(t => t.RegisteredUserTypeId)
                .Index(t => t.PrimarySchoolUserRoleId)
                .Index(t => t.SecondarySchoolUserRoleId)
                .Index(t => t.ClassId)
                .Index(t => t.GenderId)
                .Index(t => t.ReligionId)
                .Index(t => t.StudentRegFormId)
                .Index(t => t.TribeId)
                .Index(t => t.Org_OrgId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        GenderName = c.String(),
                    })
                .PrimaryKey(t => t.GenderId);
            
            CreateTable(
                "dbo.PrimarySchoolUserRoles",
                c => new
                    {
                        PrimarySchoolUserRoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.PrimarySchoolUserRoleID);
            
            CreateTable(
                "dbo.RegisteredUserOrganisations",
                c => new
                    {
                        RegisteredUserOrganisationId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        OrgId = c.Int(nullable: false),
                        OrgName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        RegUserOrgBrand = c.Int(),
                        IsTester = c.Boolean(),
                        RegisteredUserTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.RegisteredUserOrganisationId)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.OrgId);
            
            CreateTable(
                "dbo.RegisteredUserTypes",
                c => new
                    {
                        RegisteredUserTypeId = c.Int(nullable: false, identity: true),
                        RegisteredUserTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RegisteredUserTypeId);
            
            CreateTable(
                "dbo.Religions",
                c => new
                    {
                        ReligionId = c.Int(nullable: false, identity: true),
                        ReligionName = c.String(),
                    })
                .PrimaryKey(t => t.ReligionId);
            
            CreateTable(
                "dbo.SecondarySchoolUserRoles",
                c => new
                    {
                        SecondarySchoolUserRoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.SecondarySchoolUserRoleId);
            
            CreateTable(
                "dbo.StudentRegForms",
                c => new
                    {
                        StudentRegFormId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StudentRegFormId);
            
            CreateTable(
                "dbo.Tribes",
                c => new
                    {
                        TribeId = c.Int(nullable: false, identity: true),
                        TribeName = c.String(),
                    })
                .PrimaryKey(t => t.TribeId);
            
            CreateTable(
                "dbo.GroupTypes",
                c => new
                    {
                        GroupTypeId = c.Int(nullable: false, identity: true),
                        GroupTypeName = c.String(nullable: false),
                        GroupOrgTypeId = c.Int(),
                        GroupRefNumb = c.Int(),
                    })
                .PrimaryKey(t => t.GroupTypeId);
            
            CreateTable(
                "dbo.Guardians",
                c => new
                    {
                        GuardianId = c.Int(nullable: false, identity: true),
                        GuardianFirstName = c.String(),
                        GuardianLastName = c.String(),
                        GuardianFullName = c.String(),
                        GuardianEmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.GuardianId);
            
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
            
            CreateTable(
                "dbo.OrgGroups",
                c => new
                    {
                        OrgGroupId = c.Int(nullable: false, identity: true),
                        OrgId = c.Int(nullable: false),
                        GroupTypeId = c.Int(nullable: false),
                        GroupOrgTypeId = c.Int(),
                        GroupRefNumb = c.Int(),
                        GroupName = c.String(),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.OrgGroupId)
                .ForeignKey("dbo.GroupTypes", t => t.GroupTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .Index(t => t.OrgId)
                .Index(t => t.GroupTypeId);
            
            CreateTable(
                "dbo.OrgOrgTypes",
                c => new
                    {
                        OrgOrgTypeId = c.Int(nullable: false, identity: true),
                        OrgId = c.Int(nullable: false),
                        OrgTypeId = c.Int(nullable: false),
                        OrgName = c.String(),
                    })
                .PrimaryKey(t => t.OrgOrgTypeId)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .ForeignKey("dbo.OrgTypes", t => t.OrgTypeId, cascadeDelete: true)
                .Index(t => t.OrgId)
                .Index(t => t.OrgTypeId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        PostTopicId = c.Int(nullable: false),
                        OrgId = c.Int(nullable: false),
                        PostSubject = c.String(),
                        PostCreatorId = c.Int(nullable: false),
                        CreatorFullName = c.String(),
                        PostContent = c.String(),
                        PostCreationDate = c.DateTime(),
                        PostExpirtyDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Orgs", t => t.OrgId, cascadeDelete: true)
                .ForeignKey("dbo.PostTopics", t => t.PostTopicId, cascadeDelete: true)
                .Index(t => t.PostTopicId)
                .Index(t => t.OrgId);
            
            CreateTable(
                "dbo.PostTopics",
                c => new
                    {
                        PostTopicId = c.Int(nullable: false, identity: true),
                        PostTopicName = c.String(),
                    })
                .PrimaryKey(t => t.PostTopicId);
            
            CreateTable(
                "dbo.RegisteredUsersGroups",
                c => new
                    {
                        RegisteredUsersGroupsId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        OrgGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegisteredUsersGroupsId)
                .ForeignKey("dbo.OrgGroups", t => t.OrgGroupId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.OrgGroupId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.StudentGuardians",
                c => new
                    {
                        StudentGuardianId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        GuardianId = c.Int(nullable: false),
                        GuardianFirstName = c.String(),
                        GuardianLastName = c.String(),
                        GuardianFullName = c.String(),
                        GuardianEmailAddress = c.String(),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.StudentGuardianId)
                .ForeignKey("dbo.Guardians", t => t.GuardianId, cascadeDelete: true)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.GuardianId);
            
            CreateTable(
                "dbo.StudentSubjects",
                c => new
                    {
                        StudentSubjectId = c.Int(nullable: false, identity: true),
                        RegisteredUserId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        SubjectName = c.String(),
                        StudentFullName = c.String(),
                        ClassId = c.Int(),
                        FirstTermStudentGrade = c.Decimal(precision: 18, scale: 2),
                        SecondTermStudentGrade = c.Decimal(precision: 18, scale: 2),
                        ThirdTermStudentGrade = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StudentSubjectId)
                .ForeignKey("dbo.RegisteredUsers", t => t.RegisteredUserId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.RegisteredUserId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false),
                        ClassId = c.Int(nullable: false),
                        ClassTeacherId = c.Int(),
                        TaughtBy = c.String(),
                        FirstTermSubjectGrade = c.Decimal(precision: 18, scale: 2),
                        SecondTermSubjectGrade = c.Decimal(precision: 18, scale: 2),
                        ThirdTermSubjectGrade = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SubjectId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.ClassTeachers", t => t.ClassTeacherId)
                .Index(t => t.ClassId)
                .Index(t => t.ClassTeacherId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "ClassTeacherId", "dbo.ClassTeachers");
            DropForeignKey("dbo.Subjects", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.StudentSubjects", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.StudentGuardians", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.StudentGuardians", "GuardianId", "dbo.Guardians");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RegisteredUsersGroups", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.RegisteredUsersGroups", "OrgGroupId", "dbo.OrgGroups");
            DropForeignKey("dbo.Posts", "PostTopicId", "dbo.PostTopics");
            DropForeignKey("dbo.Posts", "OrgId", "dbo.Orgs");
            DropForeignKey("dbo.OrgOrgTypes", "OrgTypeId", "dbo.OrgTypes");
            DropForeignKey("dbo.OrgOrgTypes", "OrgId", "dbo.Orgs");
            DropForeignKey("dbo.OrgGroups", "OrgId", "dbo.Orgs");
            DropForeignKey("dbo.OrgGroups", "GroupTypeId", "dbo.GroupTypes");
            DropForeignKey("dbo.RegisteredUsers", "TribeId", "dbo.Tribes");
            DropForeignKey("dbo.RegisteredUsers", "StudentRegFormId", "dbo.StudentRegForms");
            DropForeignKey("dbo.RegisteredUsers", "SecondarySchoolUserRoleId", "dbo.SecondarySchoolUserRoles");
            DropForeignKey("dbo.RegisteredUsers", "ReligionId", "dbo.Religions");
            DropForeignKey("dbo.RegisteredUsers", "RegisteredUserTypeId", "dbo.RegisteredUserTypes");
            DropForeignKey("dbo.RegisteredUserOrganisations", "RegisteredUserId", "dbo.RegisteredUsers");
            DropForeignKey("dbo.RegisteredUserOrganisations", "OrgId", "dbo.Orgs");
            DropForeignKey("dbo.RegisteredUsers", "PrimarySchoolUserRoleId", "dbo.PrimarySchoolUserRoles");
            DropForeignKey("dbo.RegisteredUsers", "Org_OrgId", "dbo.Orgs");
            DropForeignKey("dbo.RegisteredUsers", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.RegisteredUsers", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Classes", "OrgId", "dbo.Orgs");
            DropForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes");
            DropForeignKey("dbo.Orgs", "OrgBrandId", "dbo.OrgBrands");
            DropForeignKey("dbo.Files", "OrgBrandId", "dbo.OrgBrands");
            DropForeignKey("dbo.Orgs", "DomainId", "dbo.Domains");
            DropForeignKey("dbo.ClassTypes", "Class_ClassId", "dbo.Classes");
            DropForeignKey("dbo.Classes", "ClassTeacherId", "dbo.ClassTeachers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Subjects", new[] { "ClassTeacherId" });
            DropIndex("dbo.Subjects", new[] { "ClassId" });
            DropIndex("dbo.StudentSubjects", new[] { "SubjectId" });
            DropIndex("dbo.StudentSubjects", new[] { "RegisteredUserId" });
            DropIndex("dbo.StudentGuardians", new[] { "GuardianId" });
            DropIndex("dbo.StudentGuardians", new[] { "RegisteredUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RegisteredUsersGroups", new[] { "OrgGroupId" });
            DropIndex("dbo.RegisteredUsersGroups", new[] { "RegisteredUserId" });
            DropIndex("dbo.Posts", new[] { "OrgId" });
            DropIndex("dbo.Posts", new[] { "PostTopicId" });
            DropIndex("dbo.OrgOrgTypes", new[] { "OrgTypeId" });
            DropIndex("dbo.OrgOrgTypes", new[] { "OrgId" });
            DropIndex("dbo.OrgGroups", new[] { "GroupTypeId" });
            DropIndex("dbo.OrgGroups", new[] { "OrgId" });
            DropIndex("dbo.RegisteredUserOrganisations", new[] { "OrgId" });
            DropIndex("dbo.RegisteredUserOrganisations", new[] { "RegisteredUserId" });
            DropIndex("dbo.RegisteredUsers", new[] { "Org_OrgId" });
            DropIndex("dbo.RegisteredUsers", new[] { "TribeId" });
            DropIndex("dbo.RegisteredUsers", new[] { "StudentRegFormId" });
            DropIndex("dbo.RegisteredUsers", new[] { "ReligionId" });
            DropIndex("dbo.RegisteredUsers", new[] { "GenderId" });
            DropIndex("dbo.RegisteredUsers", new[] { "ClassId" });
            DropIndex("dbo.RegisteredUsers", new[] { "SecondarySchoolUserRoleId" });
            DropIndex("dbo.RegisteredUsers", new[] { "PrimarySchoolUserRoleId" });
            DropIndex("dbo.RegisteredUsers", new[] { "RegisteredUserTypeId" });
            DropIndex("dbo.Files", new[] { "OrgBrandId" });
            DropIndex("dbo.Orgs", new[] { "OrgTypeId" });
            DropIndex("dbo.Orgs", new[] { "OrgBrandId" });
            DropIndex("dbo.Orgs", new[] { "DomainId" });
            DropIndex("dbo.ClassTypes", new[] { "Class_ClassId" });
            DropIndex("dbo.Classes", new[] { "ClassTeacherId" });
            DropIndex("dbo.Classes", new[] { "OrgId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Subjects");
            DropTable("dbo.StudentSubjects");
            DropTable("dbo.StudentGuardians");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RegisteredUsersGroups");
            DropTable("dbo.PostTopics");
            DropTable("dbo.Posts");
            DropTable("dbo.OrgOrgTypes");
            DropTable("dbo.OrgGroups");
            DropTable("dbo.Jobs");
            DropTable("dbo.Guardians");
            DropTable("dbo.GroupTypes");
            DropTable("dbo.Tribes");
            DropTable("dbo.StudentRegForms");
            DropTable("dbo.SecondarySchoolUserRoles");
            DropTable("dbo.Religions");
            DropTable("dbo.RegisteredUserTypes");
            DropTable("dbo.RegisteredUserOrganisations");
            DropTable("dbo.PrimarySchoolUserRoles");
            DropTable("dbo.Genders");
            DropTable("dbo.RegisteredUsers");
            DropTable("dbo.OrgTypes");
            DropTable("dbo.Files");
            DropTable("dbo.OrgBrands");
            DropTable("dbo.Domains");
            DropTable("dbo.Orgs");
            DropTable("dbo.ClassTypes");
            DropTable("dbo.ClassTeachers");
            DropTable("dbo.Classes");
        }
    }
}
