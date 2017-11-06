using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comps.DomainLayer
{

    public class Province : DelEntity
    {
        public Province()
        {
            this.Cities = new List<City>();
        }
        public string Name { get; set; }
        public virtual  ICollection<City> Cities { get; set; }
    }
}