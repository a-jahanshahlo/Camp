using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class PassengerMap : EntityTypeConfiguration<FreePassenger>
    {
        public PassengerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("Passenger");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Nid).HasColumnName("Nid");
            this.Property(t => t.UserId).HasColumnName("UserId");


            // Relationships
            this.HasOptional(t => t.User)
                .WithMany(t => t.Passengers)
                .HasForeignKey(d => d.UserId);

        }
    }
}