using System;
using Comps.DomainLayer.Security;

namespace Comps.DomainLayer
{
    public class FreePassenger : DelEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nid { get; set; }
        public string Mobile { get; set; }
        public bool Gender { get; set; }
        public int? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}