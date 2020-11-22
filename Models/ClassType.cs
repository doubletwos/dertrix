using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class ClassType
    {
        public int ClassTypeId { get; set; }

        public string ClassTypeName { get; set; }

        public Class Class { get; set; }



    }
}