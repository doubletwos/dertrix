﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
   public class StudentSubjectGrade
    {
        public int StudentSubjectGradeId { get; set; } 

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int? ClassId { get; set; }

        public int? ClassRef { get; set; }

        public int? OrgId { get; set; }

        public decimal? FirstTerm_ExamGrade { get; set; } 

        public decimal? SecondTerm_ExamGrade { get; set; } 

        public decimal? ThirdTerm_ExamGrade { get; set; }

        public decimal? FirstTerm_TestGrade { get; set; }

        public decimal? SecondTerm_TestGrade { get; set; }

        public decimal? ThirdTerm_TestGrade { get; set; }

        public DateTime? Last_updated_date { get; set; }

        public DateTime? Created_date { get; set; }



    }
}