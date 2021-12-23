using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dertrix.Models;


namespace Dertrix.ViewModels
{
    public class UpdateStudentGradesViewModel
    {
        public UpdateStudentGradesViewModel()
        {
            this.StudentSubjectGrades = new List<StudentSubjectGrade>();
        }

        public  RegisteredUser RegisteredUser { get; set; }

        public List<StudentSubjectGrade> StudentSubjectGrades { get; set; }

    }
}