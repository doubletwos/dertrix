using Dertrix.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.ViewModels
{
    public class EditPostViewModel
    {
        public EditPostViewModel() 
        {
            this.OrgGroups = new List<OrgGroup>();
        }

        public int PostId { get; set; }

        public int? PostTopicId { get; set; }
        public virtual PostTopic PostTopic { get; set; }

        public int OrgId { get; set; }
        public Org Org { get; set; }

        public string PostSubject { get; set; }

        public int PostCreatorId { get; set; }

        public string CreatorFullName { get; set; }

        public string PostContent { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PostCreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PostExpirtyDate { get; set; }

        public List<OrgGroup> OrgGroups { get; set; }

        public bool IsSelected { get; set; }

        public bool? SendAsEmail { get; set; }

    }
}