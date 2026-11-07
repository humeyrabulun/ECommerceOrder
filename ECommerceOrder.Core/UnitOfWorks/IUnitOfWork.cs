using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceOrder.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitAsync();

        void Commit();
    }
}
