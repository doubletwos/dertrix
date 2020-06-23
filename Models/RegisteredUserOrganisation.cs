using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeus.Models
{
    public class RegisteredUserOrganisation
    {
        public int RegisteredUserOrganisationId  { get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public string Email { get; set; }

        public int OrgId { get; set; }
        public Org Org { get; set; }



    }
}