using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class ReservationMap : EntityTypeConfiguration<Reservation>
    {
        public ReservationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("Reserve");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
 
            this.Property(t => t.SuiteId).HasColumnName("SuiteId");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");

 
            this.HasRequired(t => t.User)
                .WithMany(t => t.Reserves)
                .HasForeignKey(d => d.UserId);
            this.HasRequired(t => t.Suite)
                .WithMany(t => t.Reservations)
                .HasForeignKey(d => d.SuiteId);

        }
    }
}