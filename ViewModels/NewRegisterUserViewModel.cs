using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dertrix.Models;

namespace Dertrix.ViewModels
{
    public class NewRegisterUserViewModel
    {


        public RegisteredUser RegisteredUser { get; set; }
        public ICollection<Org> Orgs { get; set; }
        public ICollection<RegisteredUserType> RegisteredUserTypes { get; set; }

   


       


    }
}