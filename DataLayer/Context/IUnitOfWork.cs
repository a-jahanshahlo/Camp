using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace Camps.DataLayer.Context
{
    public interface IUnitOfWork : IDisposable
    {
    
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void ForceDatabaseInitialize();
        int SaveChanges();
        DbEntityEntry<TEntity> Update<TEntity>(TEntity val) where TEntity : class;

    }


   
}