using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class SuiteFacilityPackageMap : EntityTypeConfiguration<SuiteFacilityPackage>
    {
        public SuiteFacilityPackageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SuiteFacilityPackage");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.SuiteId).HasColumnName("SuiteId");
            this.Property(t => t.FacilityPackageId).HasColumnName("FacilityPackageId");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.FacilityPackage)
                .WithMany(t => t.SuiteFacilityPackages)
                .HasForeignKey(d => d.FacilityPackageId);
            this.HasOptional(t => t.Suite)
                .WithMany(t => t.SuiteFacilityPackages)
                .HasForeignKey(d => d.SuiteId);

        }
    }
}