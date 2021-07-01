using Dertrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.ViewModels
{
    public class EditOrgSchCalViewModel
    {
        public EditOrgSchCalViewModel() 
        {
            this.OrgGroups = new List<OrgGroup>();
        }


        public int OrgSchCalendarId { get; set; }
        public int? CalendarCategoryId { get; set; }
        public int OrgId { get; set; }
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public string CreatorFullName { get; set; }
        public string Description { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? EventTime { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? IsRecurring { get; set; }
        public int? Frequency { get; set; }
        public bool? SendAsEmail { get; set; }


        public OrgSchCalendar OrgSchCalendar { get; set; }
        public List<OrgGroup> OrgGroups { get; set; }
        public bool IsSelected { get; set; }
        public bool? Isarchived { get; set; }

        public IEnumerable<CalendarCategory> CalendarCategorys { get; set; }
    }
}