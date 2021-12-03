using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class Org_Events_Log
    {
        public int Org_Events_LogId { get; set; }

        public string Org_Event_SubjectId { get; set; }

        public string Org_Event_SubjectName { get; set;}

        public string Org_Event_TriggeredbyId { get; set; }

        public string Org_Event_TriggeredbyName { get; set; }

        public string OrgId { get; set;}


        [Display(Name = "Event date & time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dddd | dd-MM-yyyy | H:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? Org_Event_Time { get; set; }

        public Org_Events_Types Org_Events_Types { get; set; }







    }
}

