using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public  class SuiteOwner:DelEntity
    {
        public SuiteOwner()
        {
            this.Suites = new List<Suite>();
        }

        
        public string Name { get; set; }
        public virtual ICollection<Suite> Suites { get; set; }
    }
}