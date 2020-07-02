using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeus.Models
{
    public class @Class
    {

        public int ClassId { get; set; }

        public string  ClassName { get; set; }

        public int? ClassRefNumb { get; set; }

        public bool ClassIsActive  { get; set; }

        public int OrgId { get; set; }
        public Org Org { get; set; }

        public virtual ICollection<RegisteredUser> RegisteredUsers { get; set; }

        public virtual ICollection<ClassType> ClassTypes { get; set; }

       






    }
}