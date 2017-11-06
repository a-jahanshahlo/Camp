using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public   class ServiceGroup: DelEntity
    {
        public ServiceGroup()
        {
            this.Services = new List<Service>();
        }

 
        public string Name { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}