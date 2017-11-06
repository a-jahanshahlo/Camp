using System;
using System.Collections.Generic;
using System.Linq;
 

namespace Comps.ServiceLayer.Interfaces
{
    public interface IGenericService<T> : IDisposable where T : class
    {
         IDictionary<string,string> Errors { get; set; }
        T Create();
        void Add(T entity);
        void AddRange(T[] entity);
        void Attach(T entity);
        void Update(T entity);
        void Update(int id,T entity);
        void Delete(T entity);
        void Delete(T entity,bool fromDb);
        T Find(Func<T, bool> predicate);
        T Find(int id);
        bool Exists(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Func<T, bool> predicate);
        bool IsValid(T entity);
    }
}
