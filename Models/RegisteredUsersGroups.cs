﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeus.Models
{
    public class RegisteredUsersGroups
    {
        public int RegisteredUsersGroupsId { get; set; }

        public int RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public int OrgGroupId { get; set; }
        public OrgGroup OrgGroup  { get; set; }









    }
}