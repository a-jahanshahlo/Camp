using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comps.DomainLayer
{
    public class Period : DelEntity
    {
        public Period()
        {
                this.Quotas = new List<Quota>(); 
            this.Reserves = new List<Reservation>();
        }
        public string   PeriodTitle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int FestivalId { get; set; }
        public virtual Festival Festival { get; set; }
        public int CampId { get; set; }
        public virtual Camp Camp { get; set; }
        public virtual ICollection<Reservation> Reserves { get; set; }
        public virtual ICollection<Quota> Quotas { get; set; }
    }
}