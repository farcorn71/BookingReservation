using Booking.Core.Entities.BusinessEntities.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Booking.Core.Data.Repository.Abstractions
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T Add(T entity);
        T GetById(Guid id);
        T GetBySpecfied(FilterDefinition<T> filter);
        void Remove(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetListBySpecfied(FilterDefinition<T> filter);
        void Update(T entity);
    }
}
