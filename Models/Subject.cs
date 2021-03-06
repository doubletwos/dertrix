using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Subject Name Is Required")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Please Select A Class ")]
        public int ClassId { get; set; }
        public @Class @Class { get; set; }


        public int? ClassTeacherId { get; set; }
        public ClassTeacher ClassTeacher { get; set; }

        public int? SubjectOrgId { get; set; }


        public string TaughtBy { get; set; }

        public decimal? First_Term_Test_MaxGrade { get; set; }

        public decimal? Second_Term_Test_MaxGrade { get; set; }

        public decimal? Third_Term_Test_MaxGrade { get; set; }

        public decimal? First_Term_Exam_MaxGrade { get; set; }

        public decimal? Second_Term_Exam_MaxGrade { get; set; }

        public decimal? Third_Term_Exam_MaxGrade { get; set; }

        public decimal? SubjectMaxGrade 
        {
            get
            {
                return First_Term_Test_MaxGrade + Second_Term_Test_MaxGrade + Third_Term_Test_MaxGrade + First_Term_Exam_MaxGrade + Second_Term_Exam_MaxGrade + Third_Term_Exam_MaxGrade;
            }
        }

        public decimal? Subject_Min_Passmark { get; set; }  

        public DateTime? Created_date { get; set; }

        public string Creator_Id { get; set; } 

    }
}
