using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class StudentGuardian
    {
        public int StudentGuardianId { get; set; }

        public int RegisteredUserId { get; set; }

        public RegisteredUser RegisteredUser { get; set; }

        public int GuardianId { get; set; }

        public Guardian Guardian { get; set; }

        public string GuardianFirstName { get; set; }

        public string GuardianLastName { get; set; }

        public string GuardianFullName { get; set; }

        public string GuardianEmailAddress { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateAdded { get; set; }


    }
}