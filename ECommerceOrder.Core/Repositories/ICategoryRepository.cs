using System;
using System.Collections.Generic;
using System.Text;
using ECommerceOrder.Core.Models;


namespace ECommerceOrder.Core.Repositories
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Task<Category> GetCategoryWithProductsAsync(int Id);
    

    }
}
