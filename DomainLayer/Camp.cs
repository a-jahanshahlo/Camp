using System;
using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class Camp : DelEntity
    {
        public Camp()
        {

            Galleries = new List<Gallery>();
            Phones = new List<Phone>();
            Suites = new List<Suite>();
            Periods=new List<Period>();

        }
        public int  AreaSize { get; set; }
        public string  Name { get; set; }

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual  ICollection<Phone> Phones { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<Suite> Suites { get; set; }
        public virtual  IList<CampFacilitie> CampFacilities { get; set; }
        public virtual IList<Period> Periods { get; set; }

    }
}
