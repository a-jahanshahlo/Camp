using System;
using Comps.DomainLayer.Security;

namespace Comps.DomainLayer
{
    public  class Booking : DelEntity
    {
        
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}