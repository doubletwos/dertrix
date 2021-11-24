using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public class RegisteredUsersGroups
    {
        public int RegisteredUsersGroupsId { get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public int OrgGroupId { get; set; }
        public OrgGroup OrgGroup { get; set; }

        public string Email { get; set; }

        public int RegUserOrgId { get; set; }

        public int GroupTypeId { get; set; }

        public int? LinkedStudentId { get; set; }

    }
}