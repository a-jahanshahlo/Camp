namespace Comps.DomainLayer
{
    /// <summary>
    /// خدمات در قالب سرویس ارائ می شود
    /// </summary>
    public  class ServicePackage:DelEntity
    {
   
        public int PackageId { get; set; }
        public int ServiceId { get; set; }
        public int ServicePrice { get; set; }
        public virtual Package Package { get; set; }
        public virtual Service Service { get; set; }
    }
}