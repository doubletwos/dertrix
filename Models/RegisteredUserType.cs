using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class RegisteredUserType   
    {
        public int RegisteredUserTypeId { get; set; }

        [Required(ErrorMessage = "Please add user type name")]
        [Display(Name = "User Type")]
        public string RegisteredUserTypeName { get; set; }    


    }
}