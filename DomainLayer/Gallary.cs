using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comps.DomainLayer
{
    public class Gallery : DelEntity
    {
        public Gallery()
        {
            this.Suites = new List<Suite>();
        }
        [Index("IX_GalleryName",IsUnique = true)]
        [MaxLength(400)]
        public string Name { get; set; }
        public virtual IList<Binary> Files { get; set; }

        public virtual IList<Camp> Camps { get; set; }
        public virtual ICollection<Suite> Suites { get; set; }
     
    }
}