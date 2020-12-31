using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dertrix.Models;


namespace Dertrix.ViewModels
{
    public class AddNewPostViewModel
    {


        public AddNewPostViewModel()
        {
            this.OrgGroups = new List<OrgGroup>();
        }

        public Post Post { get; set; }
        public List<OrgGroup> OrgGroups { get; set; }
        public bool IsSelected { get; set; }
        public IEnumerable<PostTopic> PostTopics  { get; set; }

    }
}