using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class Department : DelEntity
    {
        public Department()
        {
            this.Quotas = new List<Quota>();
            this.DepartmentDeptRoles = new List<DepartmentDeptRole>();
        }

        public string DepTitle { get; set; }
        public virtual ICollection<DepartmentDeptRole> DepartmentDeptRoles { get; set; }
        public virtual ICollection<Quota> Quotas { get; set; }
    }
}