using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class OrgOrgType
    {
        public int OrgOrgTypeId { get; set; }

        public int OrgId { get; set; }
        public Org Org { get; set; }

        public int OrgTypeId { get; set; }
        public OrgType OrgType { get; set; }

        public string OrgName { get; set; }

      





    }
}