using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class DepartmentDeptRoleMap : EntityTypeConfiguration<DepartmentDeptRole>
    {
        public DepartmentDeptRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("DepartmentDeptRole");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.DeptRoleId).HasColumnName("DeptRoleId");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
 

            // Relationships
            this.HasRequired(t => t.Department)
                .WithMany(t => t.DepartmentDeptRoles)
                .HasForeignKey(d => d.DepartmentId);
            this.HasRequired(t => t.DeptRole)
                .WithMany(t => t.DepartmentDeptRoles)
                .HasForeignKey(d => d.DeptRoleId);

        }
    }
}