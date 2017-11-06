using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class FacilityUnitMap : EntityTypeConfiguration<FacilityUnit>
    {
        public FacilityUnitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FacilityUnits");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.UnitName).HasColumnName("UnitName");
            this.Property(t => t.UnitCount).HasColumnName("UnitCount");
        }
    }
}