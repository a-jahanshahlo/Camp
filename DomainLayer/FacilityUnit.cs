using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class FacilityUnit : DelEntity
    {
        public FacilityUnit()
        {
            this.Facilities = new List<Facility>();
        }

        
        public string UnitName { get; set; }
        public int UnitCount { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }
    }
}