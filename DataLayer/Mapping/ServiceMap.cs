using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class ServiceMap : EntityTypeConfiguration<Service>
    {
        public ServiceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ServiceName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Service");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.ServiceName).HasColumnName("ServiceName");
            this.Property(t => t.ServiceGroup).HasColumnName("ServiceGroup");

            // Relationships
            this.HasRequired(t => t.ServiceGroup1)
                .WithMany(t => t.Services)
                .HasForeignKey(d => d.ServiceGroup);

        }
    }
}