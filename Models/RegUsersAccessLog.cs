using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class RegUsersAccessLog
    {
        public int RegUsersAccessLogId { get; set; }

        public int? OrgId { get; set; }

        public string SessionId { get; set; }

        public int? RegUserId { get; set; }

        public string UserFullName { get; set; }

        [Display(Name = "Log in date & time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LogInTime { get; set; }

        [Display(Name = "Log out date & time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LogOutTime { get; set; }


    }
}