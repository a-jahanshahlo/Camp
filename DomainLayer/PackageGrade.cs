using System.Collections.Generic;

namespace Comps.DomainLayer
{
    /// <summary>
    /// درجه بسته مثلا:درجه1 و درجه 2 و لوکس
    /// </summary>
    public  class PackageGrade:DelEntity
    {
        public PackageGrade()
        {
            this.Packages = new List<Package>();
        }
        public string Name { get; set; }
        public int Grade { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
}