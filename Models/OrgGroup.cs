using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zeus.Models
{
    public class OrgGroup
    {
        public int OrgGroupId { get; set; }

        public int OrgId { get; set; }
        public Org Org { get; set; }

        public int GroupTypeId { get; set; }
        public GroupType GroupType { get; set; }

        public int? GroupOrgTypeId { get; set; }

        public int? GroupRefNumb { get; set; }

        public string  GroupName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }


    }
}