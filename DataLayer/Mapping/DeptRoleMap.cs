using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class DeptRoleMap : EntityTypeConfiguration<DeptRole>
    {
        public DeptRoleMap()
        {
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.RoleTitle)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("DeptRole");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleTitle).HasColumnName("DeptTitle");
        }
    }
}