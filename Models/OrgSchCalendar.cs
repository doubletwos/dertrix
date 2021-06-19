using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class OrgSchCalendar
    {

        public OrgSchCalendar() 
        {
            OrgGroups = new List<OrgGroup>();
        }


        public int OrgSchCalendarId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Category")]
        public int? CalendarCategoryId { get; set; }
        public virtual CalendarCategory CalendarCategory { get; set; }

        public int OrgId { get; set; }
        public Org Org { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public int CreatorId { get; set; }

        public string CreatorFullName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Event Date")]
        [DisplayFormat(DataFormatString = "{0:dddd  dd-MMMM-yyyy | H:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? EventDate { get; set; }

        [Display(Name = "Time")]
        [DisplayFormat(DataFormatString = "{0:dddd  dd-MMMM-yyyy | H:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? EventTime { get; set; }

        [Display(Name = "Created")]
        [DisplayFormat(DataFormatString = "{0:dddd | dd-MM-yyyy | H:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }

        public List<OrgGroup> OrgGroups { get; set; }

        public bool? IsRecurring { get; set; }

        public int? Frequency { get; set; }

        public bool? SendAsEmail { get; set; }

    }
}