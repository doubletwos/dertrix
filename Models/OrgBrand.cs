using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class OrgBrand
    {
        public int OrgBrandId { get; set; } 

        public string OrgBrandName { get; set; }

        public string OrgBrandButtonColour { get; set; } 

        public virtual ICollection<File> Files { get; set; }


    }
}