using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class UserServiceMap : EntityTypeConfiguration<UserService>
    {
        public UserServiceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UserService");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.ActiveDate).HasColumnName("ActiveDate");
            this.Property(t => t.ExpireDate).HasColumnName("ExpireDate");
            this.Property(t => t.PackageId).HasColumnName("PackageId");
            this.Property(t => t.UserId).HasColumnName("UserId");

            // Relationships
            this.HasRequired(t => t.Package)
                .WithMany(t => t.UserServices)
                .HasForeignKey(d => d.PackageId);
            this.HasOptional(t => t.User)
                .WithMany(t => t.UserServices)
                .HasForeignKey(d => d.UserId);

        }
    }
}