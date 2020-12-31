using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class Org
    {
        public int OrgId { get; set; }

        public string OrgName { get; set; }

        public string OrgAddress { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }

        public int DomainId { get; set; }
        public Domain Domain { get; set; }  

        public int OrgBrandId { get; set; }
        public OrgBrand OrgBrand { get; set; }

        public int? OrgTypeId { get; set; }
        public OrgType OrgType { get; set; }
 
        public string CreatedBy { get; set; }  





    }
}