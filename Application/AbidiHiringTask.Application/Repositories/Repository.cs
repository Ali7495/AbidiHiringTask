using HiringTask.Domain.Data.Context;
using HiringTask.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly HiringTaskDbContext _dbContext;
        internal DbSet<TEntity> Entities;

        public Repository(HiringTaskDbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Entities.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await Entities.AddRangeAsync(entities, cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                Entities.Update(entity);
            });
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Entities.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Entities.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Entities.FindAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                Entities.Update(entity);
            });
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() =>
            {
                Entities.UpdateRange(entities);
            });
        }
    }
}
