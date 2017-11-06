using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);



            // Table & Column Mappings
            this.ToTable("Address");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FullAddress).HasColumnName("FullAddress");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Zip).HasColumnName("Zip");
            this.Property(t => t.CityId).HasColumnName("CityId");


            // Relationships
            this.HasOptional(t => t.City)
               .WithMany(t => t.Addresses)
               .HasForeignKey(d => d.CityId);

            //this.HasRequired(t => t.Camp)
            //    .WithRequiredDependent(t => t.Addresses)
            //    .WillCascadeOnDelete(true);

        }
    }
}