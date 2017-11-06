using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using Camps.CommonLib.Exceptions;
using Camps.DataLayer.Mapping;
using Comps.DomainLayer;
using Comps.DomainLayer.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camps.DataLayer.Context
{
    public class MainContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUnitOfWork
    {
        //This overload needed to find custom connectionString in WEB Layer at web.config
        //Custom ConnectionString Must Declare in web.config in WebLayer not at this layer!!
        public MainContext()
            : base("MainContextConnection")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Database.Log = sql => Console.Write(sql);
         
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Camp> Camps { get; set; }
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<FreePassenger> Passengers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Suite> Suites { get; set; }
        public DbSet<SuiteFacilitie> SuiteFacilities { get; set; }
        public DbSet<SuiteType> SuiteTypes { get; set; }
        public DbSet<Binary> Files { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<PersonalSetting> PersonalSetting { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserService> UserServices { get; set; }
        public DbSet<ServicePackage> ServicePackages { get; set; }
        public DbSet<ServiceGroup> ServiceGroups { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageGrade> PackageGrades { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityPackage> FacilityPackages { get; set; }
        public DbSet<FacilityUnit> FacilityUnits { get; set; }
        public DbSet<ItemsInFacilityPackage> ItemsInFacilityPackages { get; set; }
        public DbSet<SuiteFacilityPackage> SuiteFacilityPackages { get; set; }
        public DbSet<SuiteOwner> SuiteOwners { get; set; }
        public DbSet<DepartmentDeptRole> DepartmentDeptRoles { get; set; }
        public DbSet<DeptRole> DeptRoles { get; set; }
        public DbSet<UserInDeptRole> DeptRoleUsers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

          //  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CampsMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new PackageGradeMap());
            modelBuilder.Configurations.Add(new PassengerMap());
            modelBuilder.Configurations.Add(new PackageMap());
            modelBuilder.Configurations.Add(new ServiceGroupMap());
            modelBuilder.Configurations.Add(new ServiceMap());
            modelBuilder.Configurations.Add(new ServicePackageMap());
            modelBuilder.Configurations.Add(new UserServiceMap());

            modelBuilder.Configurations.Add(new BookingMap());
            modelBuilder.Configurations.Add(new FacilityMap());
            modelBuilder.Configurations.Add(new FacilityPackageMap());
            modelBuilder.Configurations.Add(new FacilityUnitMap());
            modelBuilder.Configurations.Add(new ItemsInFacilityPackageMap());
 
            modelBuilder.Configurations.Add(new SuiteFacilityPackageMap());
            modelBuilder.Configurations.Add(new SuiteOwnerMap());
            modelBuilder.Configurations.Add(new FestivalMap());
            modelBuilder.Configurations.Add(new PeriodMap());
            modelBuilder.Configurations.Add(new ReservationMap());
            modelBuilder.Configurations.Add(new QuotaMap());

            modelBuilder.Configurations.Add(new DeptRoleMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new DepartmentDeptRoleMap());
            modelBuilder.Configurations.Add(new UserInDeptRoleMap());

            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new ProvinceMap());

            modelBuilder.Configurations.Add(new GenderMap());


            modelBuilder.Entity<ApplicationUser>().ToTable("Users")
                .Property(t => t.PhoneNumber)
                .HasMaxLength(20);
                //.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_PhoneNumber") { IsUnique = true })); ;
            modelBuilder.Entity<CustomRole>().ToTable("Roles");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");


        }
        public DbEntityEntry<TEntity> Update<TEntity>(TEntity val) where TEntity : class
        {
            var entity = Entry(val);
            entity.State = EntityState.Modified;
            return entity;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ForceDatabaseInitialize()
        {
            this.Database.Initialize(force: true);
        }
        // public System.Data.Entity.DbSet<RSSFeed.WebUI.ViewModel.SiteViewModel> SiteViewModels { get; set; }
        //public System.Data.Entity.DbSet<RSSFeed.WebUI.ViewModel.ChannelViewModel> ChannelViewModels { get; set; }
    }
}