using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class UserInDeptRoleMap : EntityTypeConfiguration<UserInDeptRole>
    {
        public UserInDeptRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("UserInDeptRole");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DepartmentDeptRoleId).HasColumnName("DepartmentDeptRoleId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.DepartmentDeptRole)
                .WithMany(t => t.UserInDeptRoles)
                .HasForeignKey(d => d.DepartmentDeptRoleId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserInDeptRoles)
                .HasForeignKey(d => d.UserId);

        }
    }
}