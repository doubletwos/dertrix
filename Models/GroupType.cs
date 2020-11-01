using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zeus.Models
{
    public class GroupType
    {
        public int GroupTypeId { get; set; }

        [Required]
        public string GroupTypeName { get; set; }


        public int? GroupOrgTypeId { get; set; }

        public int? GroupRefNumb { get; set; }  


    }
}