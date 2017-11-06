using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class Facility : DelEntity
    {
        public Facility()
        {
            this.ItemsInFacilityPackages = new List<ItemsInFacilityPackage>();

        }

        
        public string FacilitiyName { get; set; }
        public int? UnitId { get; set; }
        public virtual FacilityUnit FacilityUnit { get; set; }
        public virtual ICollection<ItemsInFacilityPackage> ItemsInFacilityPackages { get; set; }
    }
}