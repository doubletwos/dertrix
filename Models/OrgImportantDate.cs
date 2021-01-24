using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class OrgImportantDate
    {
        public int OrgImportantDateId { get; set; }

        public string ImportantDateName { get; set; }

        public int OrgId { get; set; }

        public string CreatedBy { get; set; }

        public string CreatorName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ImportanttDate { get; set; }




    }
}