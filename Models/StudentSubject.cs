using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zeus.Models
{
    public class StudentSubject
    {
        public int StudentSubjectId {get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }



        public int SubjectId { get; set; }
        public Subject Subject { get; set; }


        public string SubjectName { get; set; }
        public int? ClassId { get; set; }



        public string[] SelectedSubjects { get; set; }

        //public string StudentSelectedSubject { get; set; }









    }
}