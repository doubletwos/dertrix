using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zeus.Models
{
    public class Org
    {
        public int OrgId { get; set; }

        [Display(Name = "Organisation Name")]
        public string OrgName { get; set; }

        [Display(Name = "Organisation Address")]
        public string OrgAddress { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }




        [Display(Name = "Domain")]
        public int DomainId { get; set; }
        public Domain Domain { get; set; }  

        [Display(Name = "Organisation Type")]
        public int OrgTypeId { get; set; }
        public OrgType OrgType { get; set; }

        [Display(Name = "Organisation Brand")]

        public int OrgBrandId { get; set; }
        public OrgBrand OrgBrand { get; set; }

      



       
    


    }
}