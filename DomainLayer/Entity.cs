using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comps.DomainLayer
{
    public interface IEntity
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         int Id { get; set; }
    }
    public class Entity:IEntity
    {
         [Key]
        public int Id { get; set; }
    }
}