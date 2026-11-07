using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerceOrder.Core.Repositories
{
    public interface IRepository<T>where T:class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity); 

    }
}
