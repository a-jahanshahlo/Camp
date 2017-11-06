using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class PeriodMap : EntityTypeConfiguration<Period>
    {
        public PeriodMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FromDate)
                .IsRequired();

            this.Property(t => t.ToDate)
                .IsRequired();

            this.Property(t => t.FestivalId)
                .IsRequired();
            // Table & Column Mappings
            this.ToTable("Period");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.FestivalId).HasColumnName("FestivalId");
            this.Property(t => t.CampId).HasColumnName("CampId");
            // Relationships
            this.HasRequired(t => t.Festival)
                .WithMany(t => t.Periods)
                .HasForeignKey(d => d.FestivalId);
            this.HasRequired(t => t.Camp)
                .WithMany(t => t.Periods)
                .HasForeignKey(d => d.CampId);
        }
    }
}