using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class FacilityPackage : DelEntity
    {
        public FacilityPackage()
        {
            this.ItemsInFacilityPackages = new List<ItemsInFacilityPackage>();
            this.SuiteFacilityPackages = new List<SuiteFacilityPackage>();
        }

        
        public string PackageName { get; set; }
        public virtual ICollection<ItemsInFacilityPackage> ItemsInFacilityPackages { get; set; }
        public virtual ICollection<SuiteFacilityPackage> SuiteFacilityPackages { get; set; }
    }
}