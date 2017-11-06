using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Camps.CommonLib;
using Camps.DataLayer.Context;
using Comps.DomainLayer;
using Comps.ServiceLayer.Interfaces;
 

namespace Comps.ServiceLayer.EFServices
{
    public class EfGenericService<TEntity> : IGenericService<TEntity>
          where TEntity : class,IDel, IEntity, new()
    {
        public IDictionary<string,string >Errors { get; set; }
        protected IUnitOfWork Uow;
        protected IDbSet<TEntity> Entities;

        public EfGenericService(IUnitOfWork uow)
        {
            Uow = uow;
            Entities = Uow.Set<TEntity>();
            Errors=new Dictionary<string, string>();
        }

        public virtual TEntity Create()
        {
            return Entities.Create();
        }
        public virtual void Add(TEntity entity)
        {
            Entities.Add(entity);
        }
        public void Attach(TEntity entity)
        {
            Entities.Attach(entity);
        }
        public void Update(TEntity entity)
        {
            var existingEntity = Entities.Find(entity.Id);
            SimpleMapper.PropertyMap(entity, existingEntity);
        }

        public void Update(int id, TEntity entity)
        {
            var existingEntity = Entities.Find(id);
            SimpleMapper.PropertyMap(entity, existingEntity);
        }

        public void Delete(TEntity entity)
        {
            if (entity!=null)
            {
                entity.IsDeleted = true;
            }
        }

        public void Delete(TEntity entity, bool fromDb)
        {
            if (entity != null && fromDb 
            )
            {
                Entities.Remove(entity);
            }
        }


        public TEntity Find(Func<TEntity, bool> predicate)
        {
            return Entities.Where(predicate).FirstOrDefault(x => !x.IsDeleted);
        }

        public TEntity Find(int id)
        {

            return Entities.Where(x => !x.IsDeleted).FirstOrDefault(x=>x.Id==id);
        }

        public bool Exists(int id)
        {
            return Entities.Find(id) != null;

        }

        public IQueryable<TEntity> GetAll()
        {
            return Entities.Where(x => !x.IsDeleted).AsQueryable();
        }

        public IQueryable<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            return Entities.Where(predicate).Where(x => !x.IsDeleted).AsQueryable();
        }

        #region IDisposable Members
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion





        public void AddRange(TEntity[] entity)
        {
            foreach (var item in entity)
            {
                Entities.Add(item);
            }
           
        }





        public virtual  bool IsValid(TEntity entity)
        {
            return true;
        }
    }
}
