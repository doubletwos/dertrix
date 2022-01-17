using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class OrgSchPostGrp
    {
        public int OrgSchPostGrpId { get; set; } 

        public int OrgPostId { get; set; } 

        public int? OrgGroupId { get; set; } 

        public int OrgId { get; set; }
    }
}