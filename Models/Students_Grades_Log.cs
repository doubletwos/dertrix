using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class Students_Grades_Log
    {
        public int Students_Grades_LogId { get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int ClassId { get; set; }
        public @Class @Class { get; set; }
        public int ClassRef { get; set; }
        public int OrgId { get; set; }

        public decimal? FirstTerm_ExamGrade { get; set; }

        public decimal? SecondTerm_ExamGrade { get; set; }

        public decimal? ThirdTerm_ExamGrade { get; set; }

        public decimal? FirstTerm_TestGrade { get; set; }

        public decimal? SecondTerm_TestGrade { get; set; }

        public decimal? ThirdTerm_TestGrade { get; set; }

        public DateTime? Last_updated_date { get; set; }

        public decimal? First_Term_Test_MaxGrade { get; set; }

        public decimal? Second_Term_Test_MaxGrade { get; set; }

        public decimal? Third_Term_Test_MaxGrade { get; set; }

        public decimal? First_Term_Exam_MaxGrade { get; set; }

        public decimal? Second_Term_Exam_MaxGrade { get; set; }

        public decimal? Third_Term_Exam_MaxGrade { get; set; }

        public decimal? Subject_Min_Passmark { get; set; }

        public decimal? Subject_Max_Passmark { get; set; }


        public decimal? StudentSubjectGradeObtained
        {
            get
            {
                return FirstTerm_ExamGrade + SecondTerm_ExamGrade + ThirdTerm_ExamGrade + FirstTerm_TestGrade + SecondTerm_TestGrade + ThirdTerm_TestGrade;
            }
        }

        public int? ClassTeacherId { get; set; }

        public DateTime? Created_date { get; set; }

        public int Updater_Id { get; set; }

        public StudentClassChangeType StudentClassChangeType { get; set; }









}
}