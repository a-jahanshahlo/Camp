using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class ServicePackageMap : EntityTypeConfiguration<ServicePackage>
    {
        public ServicePackageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ServicePackage");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.PackageId).HasColumnName("PackageId");
            this.Property(t => t.ServiceId).HasColumnName("ServiceId");
            this.Property(t => t.ServicePrice).HasColumnName("ServicePrice");

            // Relationships
            this.HasRequired(t => t.Package)
                .WithMany(t => t.ServicePackages)
                .HasForeignKey(d => d.ServiceId);
            this.HasRequired(t => t.Service)
                .WithMany(t => t.ServicePackages)
                .HasForeignKey(d => d.ServiceId);

        }
    }
}