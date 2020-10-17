using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeus.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

    
        public int ClassId { get; set; }

        public @Class @Class { get; set; }


        public int? ClassTeacherId { get; set; }
        public ClassTeacher ClassTeacher { get; set; }
         
        public string TaughtBy { get; set; }






    }
}