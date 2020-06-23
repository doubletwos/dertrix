namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                        OrgBrandName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgBrandId);
            
            CreateTable(
                "dbo.Orgs",
                c => new
                    {
                        OrgId = c.Int(nullable: false, identity: true),
                        OrgName = c.String(),
                        OrgAddress = c.String(),
                        CreationDate = c.DateTime(),
                        DomainId = c.Int(nullable: false),
                        OrgTypeId = c.Int(nullable: false),
                        OrgBrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrgId)
                .ForeignKey("dbo.Domains", t => t.DomainId, cascadeDelete: true)
                .ForeignKey("dbo.OrgBrands", t => t.OrgBrandId, cascadeDelete: true)
                .ForeignKey("dbo.OrgTypes", t => t.OrgTypeId, cascadeDelete: true)
                .Index(t => t.DomainId)
                .Index(t => t.OrgTypeId)
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Orgs", "OrgTypeId", "dbo.OrgTypes");
            DropForeignKey("dbo.Orgs", "OrgBrandId", "dbo.OrgBrands");
            DropForeignKey("dbo.Orgs", "DomainId", "dbo.Domains");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Orgs", new[] { "OrgBrandId" });
            DropIndex("dbo.Orgs", new[] { "OrgTypeId" });
            DropIndex("dbo.Orgs", new[] { "DomainId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrgTypes");
            DropTable("dbo.Orgs");
            DropTable("dbo.OrgBrands");
            DropTable("dbo.Domains");
        }
    }
}
