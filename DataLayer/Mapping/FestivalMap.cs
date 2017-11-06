using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class FestivalMap : EntityTypeConfiguration<Festival>
    {
        public FestivalMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // Table & Column Mappings
            this.ToTable("Festival");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            this.Property(t => t.FestivalTitle).HasColumnName("FestivalTitle");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            // Relationships
            //this.HasOptional(t => t.ToDate)
            //    .WithMany(t => t.Facilities)
            //    .HasForeignKey(d => d.UnitId);

        }
    }
}