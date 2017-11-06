using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comps.DomainLayer
{
    public class DepartmentDeptRole:DelEntity
    {
        public DepartmentDeptRole()
        {
            UserInDeptRoles =new List<UserInDeptRole>();
        }
        public int DepartmentId { get; set; }
        public int DeptRoleId { get; set; }
        public virtual Department Department { get; set; }
        public virtual DeptRole DeptRole { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection< UserInDeptRole>  UserInDeptRoles { get; set; }
    }
}