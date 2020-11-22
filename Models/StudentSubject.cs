using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class StudentSubject
    {
        public int StudentSubjectId {get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }



        public int SubjectId { get; set; }
        public Subject Subject { get; set; }


        public string SubjectName { get; set; }

        public string StudentFullName { get; set; }


        public int? ClassId { get; set; }


        public decimal? FirstTermStudentGrade { get; set; }

        public decimal? SecondTermStudentGrade { get; set; }

        public decimal? ThirdTermStudentGrade { get; set; }


    }
}