using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dertrix.Models;


namespace Dertrix.ViewModels
{
    public class AddClassTimetableSlotViewModel
    {
        public AddClassTimetableSlotViewModel()
        {
            this.OrgSchDay = new List<OrgSchDay>();
            this.OrgClassPeriod = new List<OrgClassPeriod>();
        }

        public @Class @Class { get; set; }

        public List<OrgSchDay> OrgSchDay { get; set; }

        public List<OrgClassPeriod> OrgClassPeriod { get; set; }

        public IEnumerable<Subject> Subjects { get; set; } 

    }
}