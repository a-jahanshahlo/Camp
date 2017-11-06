using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Comps.DomainLayer;

namespace Camps.DataLayer.Mapping
{
    public class QuotaMap : EntityTypeConfiguration<Quota>
    {
        public QuotaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

 

            // Table & Column Mappings
            this.ToTable("Quota");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.BossUserId).HasColumnName("BossUserId").IsOptional(); 
            this.Property(t => t.DeadLineTime).HasColumnName("DeadLineTime");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.OperatorUserId).HasColumnName("OperatorUserId");
            this.Property(t => t.PassengerUserId).HasColumnName("PassengerUserId").IsOptional();
            this.Property(t => t.PeriodId).HasColumnName("PeriodId");
            this.Property(t => t.IsRefuse).HasColumnName("IsRefuse");
            this.Property(t => t.WhoRefuseId).HasColumnName("WhoRefuseId");

            // Relationships
            this.HasRequired(t => t.BossUser)
                .WithMany(t => t.BossQuotas)
                .HasForeignKey(d => d.BossUserId).WillCascadeOnDelete(false); 

            this.HasRequired(t => t.OperatorUser)
                .WithMany(t => t.OperatorQuotas)
                .HasForeignKey(d => d.OperatorUserId).WillCascadeOnDelete(false);

            this.HasRequired(t => t.PassengerUser)
                .WithMany(t => t.PassengerQuotas)
                .HasForeignKey(d => d.PassengerUserId).WillCascadeOnDelete(false);

            this.HasRequired(t => t.WhoRefuse)
                .WithMany(t => t.WhoRefuseQuotas)
                .HasForeignKey(d => d.WhoRefuseId).WillCascadeOnDelete(false); 

            this.HasRequired(t => t.Department)
                .WithMany(t => t.Quotas)
                .HasForeignKey(d => d.DepartmentId);

            this.HasRequired(t => t.Period)
                .WithMany(t => t.Quotas)
                .HasForeignKey(d => d.PeriodId);



        }
    }
}