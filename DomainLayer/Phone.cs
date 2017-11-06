using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class Phone : DelEntity
    {
        public string PhoneNumber { get; set; }
        public virtual ICollection<Camp> Camps { get; set; }
        public virtual ICollection<Suite> Suites { get; set; } 
    }   

}