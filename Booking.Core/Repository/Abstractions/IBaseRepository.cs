using Booking.Core.Entities.BusinessEntities.Base;
using System;
using System.Collections.Generic;

namespace Booking.Core.Data.Repository.Abstractions
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T Add(T entity);
        T GetById(Guid id);
        void Remove(Guid id);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}
