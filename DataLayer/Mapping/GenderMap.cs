using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class GenderMap : EntityTypeConfiguration<Gender>
    {
        public GenderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("Gender");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.Name).HasColumnName("Name");


        }
    }
}