namespace Comps.DomainLayer
{
    public class ItemsInFacilityPackage : DelEntity
    {
        
        public int? NumberPerItem { get; set; }
        public int? FacilityPackageId { get; set; }
        public int? FacilityId { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual FacilityPackage FacilityPackage { get; set; }
    }
}