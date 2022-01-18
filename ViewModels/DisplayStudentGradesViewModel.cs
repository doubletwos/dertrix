using Dertrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.ViewModels
{
    public class DisplayStudentGradesViewModel
    {
        public DisplayStudentGradesViewModel()
        {
            this.StudentSubjectGrades = new List<StudentSubjectGrade>();
            this.Subject = new List<Subject>();

        }

        public RegisteredUser RegisteredUser { get; set; }

        public List<StudentSubjectGrade> StudentSubjectGrades { get; set; }

        public List<Subject> Subject { get; set; }

    }
}