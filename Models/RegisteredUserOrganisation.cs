using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class RegisteredUserOrganisation
    {
      
        public int RegisteredUserOrganisationId  { get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

   
        public int OrgId { get; set; }
        public Org Org { get; set; }

        public string OrgName { get; set; }

        public int? TitleId { get; set; }
        public virtual Title Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactFullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string Email { get; set; }

        public int? RegUserOrgBrand { get; set; }

        public bool? IsTester { get; set; }

        public int? RegisteredUserTypeId { get; set; }

        public int? PrimarySchoolUserRoleId { get; set; }
        public PrimarySchoolUserRole PrimarySchoolUserRole { get; set; }


        public int? SecondarySchoolUserRoleId { get; set; }
        public SecondarySchoolUserRole SecondarySchoolUserRole { get; set; }

        public int? NurserySchoolUserRoleId { get; set; }
        public NurserySchoolUserRole NurserySchoolUserRole { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dddd | dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EnrolmentDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dddd | dd-MM-yyyy | H:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? LastLogOn { get; set; } 


        public string CreatedBy { get; set; }

        public string FullName { get; set; }


        public RegistrationFlags RegistrationFlags  { get; set; }











    }
}