using HiringTask.Domain.Data.Context;
using HiringTask.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
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

        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Entities.AddAsync(entity, cancellationToken);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await Entities.AddRangeAsync(entities, cancellationToken);
        }

        public void Delete(TEntity entity)
        {
            Entities.Update(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                Entities.Update(entity);
            });
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.AsNoTracking().Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Entities.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.AsNoTracking().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Entities.AsNoTracking().ToListAsync(cancellationToken);
        }

        public TEntity GetById(Guid id)
        {
            return Entities.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Entities.FindAsync(id, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            Entities.Update(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                Entities.Update(entity);
            });
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
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
