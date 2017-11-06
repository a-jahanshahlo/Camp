using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Comps.DomainLayer.Security;

namespace Comps.DomainLayer
{
    public class PersonalSetting 
    {
        public PersonalSetting()
        {
            DesktopImage = "/content/DesktopImage/1.jpg";
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string DesktopImage { get; set; }
        [DisplayName("کاربری ")]
        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}