﻿using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;
using System.Web.WebPages.Html;
using System.Xml.Linq;

namespace Dertrix.Models
{
    public class RegisteredUser
    {
       

        public int RegisteredUserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public string OtherNames { get; set; }


        public string ContactFullName
        {
            get
            {
                return FirstName + " " + OtherNames + " " + LastName;
            }
        }

        public string FullName { get; set; }


        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "email address is required")]
        public string Email { get; set; }

        public string LoginErrorMsg { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }



        [Display(Name = "Telephone Number ")]
        public string Telephone { get; set; }


      
        public int RegisteredUserTypeId{ get; set; }
        [Display(Name = "User Type ")]
        public RegisteredUserType RegisteredUserType { get; set; }



        public int? PrimarySchoolUserRoleId { get; set; }
        [Display(Name = "Role ")]
        public PrimarySchoolUserRole PrimarySchoolUserRole { get; set; }

        public int? SecondarySchoolUserRoleId { get; set; }
        [Display(Name = "Role ")]
        public SecondarySchoolUserRole SecondarySchoolUserRole { get; set; }

        public int? NurserySchoolUserRoleId { get; set; }
        [Display(Name = "Role ")]
        public NurserySchoolUserRole NurserySchoolUserRole { get; set; }



        public Org Org { get; set; }

        public int? SelectedOrg { get; set; }
        public ICollection<int> SelectedOrgList { get; set; }

        public ICollection<RegisteredUserOrganisation> RegisteredUserOrganisations { get; set; }


        public int? ClassId { get; set; }    
        public virtual Class Class { get; set; }

        public int? ClassRef { get; set; }

        public int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        public int? ReligionId { get; set; }
        public virtual Religion Religion { get; set; }

        public int? TitleId { get; set; }
        public virtual Title Title{ get; set; }

        public int? RelationshipId { get; set; }
        public virtual Relationship Relationship { get; set; }

        public int? StudentRegFormId { get; set; }
        public StudentRegForm StudentRegForm { get; set; }


     
        public bool? IsTester { get; set; }


        public int? TribeId { get; set; }
        public virtual Tribe Tribe { get; set; }


        [Display(Name = "Date Of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

     
        [Display(Name = "Enrolment Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EnrolmentDate  { get; set; }

        public string CreatedBy { get; set; }

        public string InviteKey { get; set; } 

        public int? RegUserOrgBrand { get; set; }

        public int? TempIntHolder { get; set; }

        public int? PgCount { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? InviteSentDate { get; set; }

        public int? CountOfInvite { get; set; }

        public bool? IsRegistered { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? RegisteredDate { get; set; }






























    }
}