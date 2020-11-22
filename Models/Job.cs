using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class Job
    {
        public int JobId { get; set; }

        public string JobName { get; set; }

        public int? JobCreatorId { get; set; }

        public string JobStatus { get; set; }

        [Display(Name = "Job Start Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }

        [Display(Name = "Job Start Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }

        public int? OrgId { get; set; } 


    }
}