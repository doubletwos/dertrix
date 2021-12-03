using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dertrix.Models
{
    public enum Org_Events_Types
    {
        Registered_Student = 1,
        Registered_Guardian = 2,
        Registered_Staff = 3,
        Deregistered_Student = 4,
        Deregistered_Guardian = 5,
        Deregistered_Staff = 6,
        Calendar_Event_Created = 7,
        Calendar_Event_Edited = 8,
        Calendar_Event_Deleted = 9
    }
}