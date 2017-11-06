using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class SuiteMap : EntityTypeConfiguration<Suite>
    {
        public SuiteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

 
            // Table & Column Mappings
            this.ToTable("Suite");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.SuiteNumber).HasColumnName("SuiteNumber");
            this.Property(t => t.SuiteName).HasColumnName("Name");
            this.Property(t => t.SuiteGradeId).HasColumnName("Grade");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.SuiteTypeId).HasColumnName("SuiteTypeId");

            this.Property(t => t.RoomCount).HasColumnName("RoomCount");
            this.Property(t => t.Capacity).HasColumnName("Capacity");

            this.Property(t => t.CampId).HasColumnName("CampId");
            this.Property(t => t.GalleryId).HasColumnName("GalleryId");
            this.Property(t => t.SuiteOwnerId).HasColumnName("SuiteOwnerId");

            // Relationships
            this.HasRequired(t => t.Camp)
                .WithMany(t => t.Suites)
                .HasForeignKey(d => d.CampId);
            this.HasOptional(t => t.Gallery)
                .WithMany(t => t.Suites)
                .HasForeignKey(d => d.GalleryId);
            this.HasRequired(t => t.SuiteOwner)
                .WithMany(t => t.Suites)
                .HasForeignKey(d => d.SuiteOwnerId);
        }
    }
}
