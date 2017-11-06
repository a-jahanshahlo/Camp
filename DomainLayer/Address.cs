
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comps.DomainLayer
{
    public enum FileType
    {
        Image,
        Video,
        Audio,
        Binary
    }
    public class Address : DelEntity
    {
        public Address()
        {
            this.Camps = new List<Camp>();
        }
        public Nullable<int> CityId { get; set; }
        public virtual City City { get; set; }
        public string FullAddress { get; set; }

        [Required, MaxLength(3)]
        public string State { get; set; }
        [Required, MaxLength(20)]
        public string Zip { get; set; }
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public virtual ICollection<Camp> Camps { get; set; }
   
    }
}