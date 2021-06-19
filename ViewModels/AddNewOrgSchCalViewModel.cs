using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dertrix.Models;


namespace Dertrix.ViewModels
{
    public class AddNewOrgSchCalViewModel
    {
        public AddNewOrgSchCalViewModel()
        {
            this.OrgGroups = new List<OrgGroup>();
        }

        public OrgSchCalendar OrgSchCalendar { get; set; }
        public List<OrgGroup> OrgGroups { get; set; }
        public bool IsSelected { get; set; }
        public IEnumerable<CalendarCategory> CalendarCategorys { get; set; }
    }
}