namespace Comps.DomainLayer
{
    public class SuiteFacilityPackage : DelEntity
    {
        
        public int? SuiteId { get; set; }
        public int? FacilityPackageId { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
        public virtual FacilityPackage FacilityPackage { get; set; }
        public virtual Suite Suite { get; set; }
    }
}