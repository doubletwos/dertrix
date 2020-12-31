using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class @Class
    {

        public int ClassId { get; set; }

        [Display(Name = "Class Name")]
        public string  ClassName { get; set; }

        public int? ClassRefNumb { get; set; }

        public bool ClassIsActive  { get; set; }

        public int OrgId { get; set; }
        public Org Org { get; set; }



        public int? ClassTeacherId { get; set; }
        public ClassTeacher ClassTeacher { get; set; }


        public string ClassTeacherFullName { get; set; }


        public virtual ICollection<RegisteredUser> RegisteredUsers { get; set; }

        public virtual ICollection<ClassType> ClassTypes { get; set; }


  

    }
}