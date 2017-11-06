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
    public class CampsMap : EntityTypeConfiguration<Camp>
    {
        public CampsMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
 

            // Table & Column Mappings
            this.ToTable("Camps");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AreaSize).HasColumnName("AreaSize");
 
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.AddressId).HasColumnName("AddressId");
         



            // Relationships

        
            this.HasOptional(t => t.Address)
                .WithMany(t => t.Camps)
                .HasForeignKey(d => d.AddressId);
         //this.HasRequired(t => t.Addresses)
         //       .WithRequiredPrincipal(t => t.Camp)
         //       .HasForeignKey(d => d.AddressId);

            //this.HasRequired(t => t.Addresses)
            //                .WithRequiredDependent(t => t.Camp)
            //                .WillCascadeOnDelete(true);
        
        }
    }
}
