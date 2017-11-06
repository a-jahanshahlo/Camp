using System;
using Comps.DomainLayer.Security;

namespace Comps.DomainLayer
{
    public class Reservation : DelEntity
    {
        public int UserId { get; set; }
 
        public int SuiteId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
 
        public virtual ApplicationUser User { get; set; }
        public virtual Suite Suite { get; set; }
    }

}