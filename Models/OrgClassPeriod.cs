using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class OrgClassPeriod
    {
        public int OrgClassPeriodId { get; set; }

        public int? ClassId { get; set; }

        public int? ClassRef { get; set; }

        public int? OrgId { get; set; }

        public int? OrgSchDayId { get; set; } 
        public OrgSchDay OrgSchDay { get; set; }  

        public string Period_1 { get; set; }

        public string Period_2{ get; set; }

        public string Period_3 { get; set; }

        public string Period_4 { get; set; }

        public string Period_5 { get; set; }

        public string Period_6 { get; set; }

        public string Period_7 { get; set; }

        public string Period_8 { get; set; }

        public int? Updater_Id { get; set; }

        public DateTime? Last_updated_date { get; set; }
        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }



    }
}