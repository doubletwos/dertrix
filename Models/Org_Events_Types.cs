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
        Calendar_Event_Deleted = 9,
        Created_Post = 10,
        Edited_Post = 11,
        Deleted_Post = 12,
        Created_Group = 13,
        Edited_Group = 14,
        Deleted_Group = 15,
        Changed_Class = 16,
        Assigned_Teacher_ = 17,
        Unassigned_Teacher_From_Class = 18,
        Assigned_Teacher = 19,
        Unassigned_Teacher = 20,
        Archived_Post = 21,
        UnArchived_Post = 22

    }
}