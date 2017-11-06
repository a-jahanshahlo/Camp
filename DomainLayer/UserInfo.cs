using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Comps.DomainLayer.Base;
using Comps.DomainLayer.Security;

namespace Comps.DomainLayer
{
    public class UserInfo : BasePerson, IDel, IEntity
    {
         [Column("Id", Order = 0)]
        [Key]
        public int Id { get; set; }
        //This can be mobile
        [Column("Phone", Order = 7)]
        public string Phone { get; set; }
         [Column("Address", Order = 8)]
        public string Address { get; set; }
         [Column("IsDeleted", Order = 9)]
        public bool IsDeleted { get; set; }




        [DisplayName("کاربری ")]
        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}