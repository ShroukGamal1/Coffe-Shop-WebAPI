using Coffe_Shop_WebAPI.Interface;
using Coffe_Shop_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Coffe_Shop_WebAPI.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class 
    {
        CoffeeContext context;
        public GenericRepository(CoffeeContext context )
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public List<T>? getElements(Func<T, bool> predicate, string? include)
        {
            if (include == null)
            {
                return context.Set<T>().Where(predicate).ToList();
            }
              return context.Set<T>().Include(include).Where(predicate).ToList();
            
        }

        public T? getElement(Func<T, bool> predicate, string? include)
        {
            if (include == null)
            {
                return context.Set<T>().Where(predicate).FirstOrDefault();
            }
            
                return context.Set<T>().Include(include).Where(predicate).FirstOrDefault();
            
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
