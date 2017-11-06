using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class ItemsInFacilityPackageMap : EntityTypeConfiguration<ItemsInFacilityPackage>
    {
        public ItemsInFacilityPackageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ItemsInFacilityPackage");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.NumberPerItem).HasColumnName("NumberPerItem");
            this.Property(t => t.FacilityPackageId).HasColumnName("FacilityPackageId");
            this.Property(t => t.FacilityId).HasColumnName("FacilityId");

            // Relationships
            this.HasOptional(t => t.Facility)
             .WithMany(t => t.ItemsInFacilityPackages)
             .HasForeignKey(d => d.FacilityId);
            this.HasOptional(t => t.FacilityPackage)
                .WithMany(t => t.ItemsInFacilityPackages)
                .HasForeignKey(d => d.FacilityPackageId);

        }
    }
}