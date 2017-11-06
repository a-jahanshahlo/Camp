using System.Collections.Generic;

namespace Comps.DomainLayer
{
    /// <summary>
    /// هر بشته شامل خدمات مختلف است
    /// </summary>
    public class Service : DelEntity
    {
        public Service()
        {
            this.ServicePackages = new List<ServicePackage>();
        }
        public string ServiceName { get; set; }
        public int ServiceGroup { get; set; }
        public virtual ServiceGroup ServiceGroup1 { get; set; }
        public virtual ICollection<ServicePackage> ServicePackages { get; set; }

    }
}