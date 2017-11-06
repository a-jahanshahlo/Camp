using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Comps.DomainLayer.Security;
using Microsoft.AspNet.Identity.EntityFramework;
 

namespace Camps.DataLayer.Context
{
    //public class ApplicationDbContext1 :
    //    IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
    //    IUnitOfWork
    //{
    //    //public DbSet<Category> Categories { set; get; }
    //    //public DbSet<Product> Products { set; get; }
    //    //public DbSet<Address> Addresses { set; get; }
    //    static ApplicationDbContext()
    //    {
    //        Database.SetInitializer(strategy: new MigrateDatabaseToLatestVersion<ApplicationDbContext, ConfigurationIdentity>());
    //    }
    //    /// <summary>
    //    /// It looks for a connection string named connectionString1 in the web.config file.
    //    /// </summary>
    //    public ApplicationDbContext()
    //        : base("SabaContextConnection")
    //    {
    //        //this.Database.Log = data => System.Diagnostics.Debug.WriteLine(data);
    //    }

    //    /// <summary>
    //    /// To change the connection string at runtime. See the SmObjectFactory class for more info.
    //    /// </summary>
    //    //public ApplicationDbContext(string connectionString)
    //    //    : base(connectionString)
    //    //{
    //    //    //Note: defaultConnectionFactory in the web.config file should be set.
    //    //}

    //    protected override void OnModelCreating(DbModelBuilder builder)
    //    {
    //        base.OnModelCreating(builder);
    //        builder.Entity<TestPhoneCode>().ToTable("TestPhoneCode");

    //        builder.Entity<ApplicationUser>().ToTable("Users")
    //            .Property(t => t.PhoneNumber);
    //          //  .HasMaxLength(20)
    //           // .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_PhoneNumber") { IsUnique = true })); ;
    //        builder.Entity<CustomRole>().ToTable("Roles");
    //        builder.Entity<CustomUserClaim>().ToTable("UserClaims");
    //        builder.Entity<CustomUserRole>().ToTable("UserRoles");
    //        builder.Entity<CustomUserLogin>().ToTable("UserLogins");
    //    }

    //    public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
    //    {
    //        return base.Set<TEntity>();
    //    }

    //    public int SaveAllChanges()
    //    {
    //        return base.SaveChanges();
    //    }

    //    public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    //    {
    //        return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
    //    }

    //    public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
    //    {
    //        Entry(entity).State = EntityState.Modified;
    //    }

    //    public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
    //    {
    //        return Database.SqlQuery<T>(sql, parameters).ToList();
    //    }

    //    public void ForceDatabaseInitialize()
    //    {
    //        this.Database.Initialize(force: true);
    //    }

    //    public DbEntityEntry<TEntity> Update<TEntity>(TEntity val) where TEntity : class
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}
