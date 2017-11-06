using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class Package : DelEntity
    {
        public Package()
        {
            this.ServicePackages = new List<ServicePackage>();
            this.UserServices = new List<UserService>();
        }

 
        public string Name { get; set; }
        public int Grade { get; set; }
        public virtual PackageGrade PackageGrade { get; set; }
        public virtual ICollection<ServicePackage> ServicePackages { get; set; }
        public virtual ICollection<UserService> UserServices { get; set; }
    }
}