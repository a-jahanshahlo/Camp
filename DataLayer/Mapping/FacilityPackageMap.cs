using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class FacilityPackageMap : EntityTypeConfiguration<FacilityPackage>
    {
        public FacilityPackageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

 

            // Table & Column Mappings
            this.ToTable("FacilityPackage");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PackageName).HasColumnName("PackageName");
        }
    }
}