using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class OrgEvent
    {
        public int OrgEventId { get; set; }

        public string EventName { get; set; }

        public int OrgId { get; set; }

        public string CreatedBy { get; set; }

        public string CreatorName { get; set; }

        public string EventDescription { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EventDate { get; set; }

        public List<OrgGroup> OrgGroups { get; set; }

        public bool? SendAsEmail { get; set; }



    }
}