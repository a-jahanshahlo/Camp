using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comps.DomainLayer
{
    public class DeptRole : DelEntity
    {
        public DeptRole()
        {
            this.DepartmentDeptRoles = new List<DepartmentDeptRole>();
 
        
        }
        [Required]
        [Display(Name = "عنوان پست")]
        public string RoleTitle { get; set; }
        public virtual ICollection<DepartmentDeptRole> DepartmentDeptRoles { get; set; }
 
    }
}