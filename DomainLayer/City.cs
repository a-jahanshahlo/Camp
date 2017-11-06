using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class City : DelEntity
    {
        public City()
        {
           Addresses=new List<Address>();
        }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual  Province Province { get; set; }
    }
}