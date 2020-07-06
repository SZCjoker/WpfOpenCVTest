using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WpfOpenCVTest.Dal.Interface;

namespace WpfOpenCVTest.Dal.Repository
{
    class GenericRepository<T> : IRepository<T> where T : class
    {

        private readonly DatabaseContext _context;


        public GenericRepository()
        { }
        public GenericRepository(DatabaseContext context)
        { 
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            _context = context;
        }


        public void Create(T instance)
        {
            if (instance == null) throw new ArgumentNullException("instance is null");
            _context.Set<T>().Add(instance);
            SaveChanges();
        }

        public void CreateOrUpdate(Expression<Func<T, bool>> predicate, T instance)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
             var Target =_context.Set<T>().FirstOrDefault(predicate);
            if (Target == null) throw new ArgumentNullException("The Target doesn't exist");
            _context.Set<T>().Remove(Target);
            SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable().AsNoTracking();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
               var errMsg =  ex.Message;
                throw;
            }
        }

        public void Update(T instance)
        {
            if (instance == null) throw new ArgumentNullException("context");
            _context.Entry(instance).State = EntityState.Modified;
            SaveChanges();
        }
    }
}
