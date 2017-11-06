using System;
using Comps.DomainLayer.Security;

namespace Comps.DomainLayer
{
    public class UserService:DelEntity
    {
       
        public DateTime ExpireDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public int? UserId { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
 
 
        public virtual ApplicationUser User { get; set; }
    }
}