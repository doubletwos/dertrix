using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class StudentGuardian
    {
        public int StudentGuardianId { get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public int? TitleId { get; set; }
        public virtual Title Title { get; set; }

        public int? RelationshipId { get; set; }
        public virtual Relationship Relationship { get; set; }

        public string GuardianFirstName { get; set; }

        public string GuardianLastName { get; set; }

        public string GuardianFullName { get; set; }

        public string GuardianEmailAddress { get; set; }

        public string Telephone { get; set; }

        public int StudentId { get; set; }
        public string StudentFullName { get; set; }

        public int? Stu_class_Org_Grp_id { get; set; }

        public bool? IsRegistered { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegisteredDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastLogOn { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InviteSentDate { get; set; }

        public int? CountOfInvite { get; set; }











        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateAdded { get; set; }

        public int OrgId { get; set; } 


    }
}