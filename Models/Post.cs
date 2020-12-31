using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dertrix.Models
{
    public class Post
    {

        public Post()
        {
            OrgGroups = new List<OrgGroup>();
        }



        public int PostId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Topic")]
        public int? PostTopicId { get; set; }
        public virtual PostTopic PostTopic { get; set; }

        public int OrgId { get; set; }
        public Org Org { get; set; }

        [Display(Name = "Subject")]
        public string PostSubject { get; set; }

        public int PostCreatorId { get; set; }

        public string CreatorFullName { get; set; }

        [AllowHtml]
        [Display(Name = "Content")]
        public string  PostContent { get; set; }

        [Display(Name = "Created")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PostCreationDate { get; set; }

        [Display(Name = "Expires")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PostExpirtyDate { get; set; }

        public List<OrgGroup> OrgGroups { get; set; }

        public bool? SendAsEmail { get; set; }


    }
}