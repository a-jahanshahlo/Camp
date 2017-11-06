using System;
using Comps.DomainLayer.Security;

namespace Comps.DomainLayer
{
    public class UserInDeptRole : DelEntity
    {

        public int DepartmentDeptRoleId { get; set; }
      
        public int UserId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual DepartmentDeptRole DepartmentDeptRole { get; set; }
        public bool IsConfirmer { get; set; }
        public bool IsActive { get; set; }
    }
}