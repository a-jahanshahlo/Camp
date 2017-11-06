using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class FacilityMap : EntityTypeConfiguration<Facility>
    {
        public FacilityMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
 

            // Table & Column Mappings
            this.ToTable("Facility");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.FacilitiyName).HasColumnName("FacilitiyName");
            this.Property(t => t.UnitId).HasColumnName("UnitId");

            // Relationships
            this.HasOptional(t => t.FacilityUnit)
                .WithMany(t => t.Facilities)
                .HasForeignKey(d => d.UnitId);

        }
    }
}