using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dertrix.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Org>Orgs { get; set; }
        public DbSet<OrgBrand> OrgBrands { get; set; }
        public DbSet<OrgType> OrgTypes {get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<RegisteredUserType> RegisteredUserTypes { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<RegisteredUserOrganisation> RegisteredUserOrganisations { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassType> ClassTypes { get; set; }
        public DbSet<Tribe> Tribes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<PrimarySchoolUserRole> PrimarySchoolUserRoles { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<SecondarySchoolUserRole> SecondarySchoolUserRoles { get; set; }
        public DbSet<StudentRegForm> StudentRegForm { get; set; }
        public DbSet<OrgOrgType> OrgOrgTypes { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ClassTeacher> ClassTeachers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTopic> PostTopics { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubject { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<OrgGroup> OrgGroups { get; set; }
        public DbSet<RegisteredUsersGroups> RegisteredUsersGroups { get; set; }
        public DbSet<StudentGuardian> StudentGuardians { get; set; }
        public DbSet<RegUsersAccessLog> RegUsersAccessLogs { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Org_Events_Log> Org_Events_Logs { get; set; }
        public DbSet<NurserySchoolUserRole> NurserySchoolUserRoles { get; set; }
        public DbSet<OrgSchCalendar> OrgSchCalendars { get; set; }
        public DbSet<CalendarCategory> CalendarCategorys { get; set; }
        public DbSet<OrgSchCalndrGrp> OrgSchCalndrGrps { get; set; } 

























        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}