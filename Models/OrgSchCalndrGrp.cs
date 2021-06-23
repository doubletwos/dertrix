using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class OrgSchCalndrGrp
    {
        public int OrgSchCalndrGrpId { get; set; }

        public int OrgSchCalendarId { get; set; }

        public int? OrgGroupId { get; set; } 

        public int OrgId { get; set; }
      
    }
}