using BookingRoomAPI.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookingRoomAPI.Domain.Interfaces.Base
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, string includeProps = null);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
    }
}
