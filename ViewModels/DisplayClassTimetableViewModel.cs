using Dertrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.ViewModels
{
    public class DisplayClassTimetableViewModel
    {
        public DisplayClassTimetableViewModel()
        {
            this.OrgClassPeriod = new List<OrgClassPeriod>();
            this.Subject = new List<Subject>();


        }

        public Class @Class { get; set; }

        public List<OrgClassPeriod> OrgClassPeriod { get; set; }

        public List<Subject> Subject { get; set; }


    }
}