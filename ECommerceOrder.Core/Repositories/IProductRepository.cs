using System;
using System.Collections.Generic;
using System.Text;
using ECommerceOrder.Core.Models;

namespace ECommerceOrder.Core.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<Product> GetProductWithDetailsAsync(int Id);
    }
}
