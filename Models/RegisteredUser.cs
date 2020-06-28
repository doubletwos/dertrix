using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;
using System.Web.WebPages.Html;
using System.Xml.Linq;

namespace Zeus.Models
{
    public class RegisteredUser
    {
       

        public int RegisteredUserId { get; set; }

        [Display(Name = "User's First Name")]
        [Required(ErrorMessage = "Your first name is required")]
        public string FirstName { get; set; }

        [Display(Name = "User's Last Name")]
        [Required(ErrorMessage = "Your last name is required")]
        public string LastName { get; set; }

        public string ContactFullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please provide your email address")]
        public string Email { get; set; }

        public string LoginErrorMsg { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }



        [Display(Name = "Mobile Telephone Number ")]
        public string Telephone { get; set; }


      
        public int RegisteredUserTypeId{ get; set; }
        [Display(Name = "User Type ")]
        public RegisteredUserType RegisteredUserType { get; set; }


        public Org Org { get; set; }

        public int SelectedOrg { get; set; }     

        public ICollection<int> SelectedOrgList { get; set; }
        public ICollection<RegisteredUserOrganisation> RegisteredUserOrganisations { get; set; }



       








    }
}