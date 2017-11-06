using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comps.DomainLayer.Base
{
    public class BasePerson
    {
        [Column("FirstName", Order = 1)]
        public string FirstName { get; set; }
        [Column("LastName", Order = 2)]
        public string LastName { get; set; }
        [Column("Age", Order = 3)]
        public int Age { get; set; }
        [Column("Nid", Order = 4)]
        public string Nid { get; set; }
        [Column("GenderId", Order = 5)]
        [Required]
        public int GenderId { get; set; }

        public virtual Gender Gender { get; set; }
        [Column("Birthday", Order = 6, TypeName = "DateTime2")]
        public DateTime Birthday { get; set; }

    }
}
