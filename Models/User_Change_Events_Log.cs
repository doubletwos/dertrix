using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class User_Change_Events_Log
    {
        public int User_Change_Events_LogId { get; set; }

        public int RegUserId { get; set; }

        public int ChangedBy { get; set; } 

        public string Old_Value { get; set; }

        public string New_Value { get; set; }

        public string OrgId { get; set; }

        [Display(Name = "Event date & time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dddd | dd-MM-yyyy | H:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? User_Change_Event_Time { get; set; }

        public User_Change_Events_Types User_Change_Events_Types { get; set; } 

    }
}