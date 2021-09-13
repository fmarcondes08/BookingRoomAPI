using BookingRoomAPI.Domain.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using BookingRoomAPI.Domain.Models.Base;

namespace BookingRoomAPI.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _context.Set<T>());
        }

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            entity.Active = true;

            var result = await DbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<T> Get(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, string includeProps = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includeProps))
            {
                query = includeProps.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (q, p) => q.Include(p));
            }

            return await query.ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            var result = DbSet.Update(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
