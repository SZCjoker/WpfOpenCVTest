using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WpfOpenCVTest.Dal.Interface
{
 public   interface IRepository<T> : IDisposable where T:class
    {
        void Create(T instance);
        void Update(T instance);
        void CreateOrUpdate(Expression<Func<T, bool>> predicate, T instance);
        T Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        void Delete(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
