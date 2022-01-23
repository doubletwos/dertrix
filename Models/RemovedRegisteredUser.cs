using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class RemovedRegisteredUser
    {
        public int RemovedRegisteredUserId { get; set; }
        public int RegisteredUserId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int? RegisteredUserType { get; set; }
        public int? PrimarySchoolUserRole { get; set; }
        public int? SecondarySchoolUserRole { get; set; }
        public int? NurserySchoolUserRole { get; set; }
        public int? OrgId { get; set; }
        public int? ClassId { get; set; }
        public int? ClassRef { get; set; }
        public int? GenderId { get; set; }
        public int? ReligionId { get; set; }
        public int? StudentRegFormId { get; set; }
        public bool IsTester { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? EnrolmentDate { get; set; }
        public DateTime? LastLogOn { get; set; }

        public int? EnrolledBy { get; set; }





























    }
}