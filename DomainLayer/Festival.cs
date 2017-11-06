using System;
using System.Collections.Generic;

namespace Comps.DomainLayer
{
    public class Festival : DelEntity
    {
        public Festival()
        {
            this.Periods = new List<Period>();
        }
        public string FestivalTitle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Period> Periods { get; set; }

    }
}