using Coffe_Shop_WebAPI.Interface;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace Coffe_Shop_WebAPI.UnitOfWork
{
    public class unitOfWork<T> where T : class
    {
        CoffeeContext context;
        private IRepository<T> entity ;
              public unitOfWork(CoffeeContext context)
        {
            this.context = context;
        }
        public IRepository<T> Entity
        {
            get
            {
                return entity ?? (entity = new GenericRepository<T>(context));
            }
        }
        public void savechanges()
        {
            context.SaveChanges();
        }

    }
}
