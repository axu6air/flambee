using Flambee.Core;
using Flambee.Core.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Flambee.Data
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> _entities;

        public EntityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public async Task<IList<TEntity>> GetByIdsAsync(IList<object> ids)
        {
            return await _context.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        public async Task<TEntity> GetByProperty(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            query = func != null ? func(query) : query;

            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }

        public async Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            query = func != null ? await func(query) : query;

            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<IList<TEntity>> InsertAsync(IList<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            transaction.Complete();

            return entities;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = EntityState.Modified;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IList<TEntity>> UpdateAsync(IList<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            _context.Entry(entities).State = EntityState.Modified;
            _context.Update(entities);
            await _context.SaveChangesAsync();
            transaction.Complete();
            return entities;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _context.Set<TEntity>().Where(predicate).ToListAsync();
            if (result.Count > 0)
                _context.RemoveRange(result);
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> ExecuteStoredProcedureAsync(string procedureName, params object[] parameters)
        {
            return await _context.Set<TEntity>().FromSqlRaw(procedureName, parameters).Select(x => (TEntity)x).ToListAsync();
        }

        public async Task<List<object>> ExecuteProcedureObjectAsync(string procedureName, params object[] parameters)
        {
            return await _context.Set<object>().FromSqlRaw(procedureName, parameters)
                       .Select(e => (object)e)
                       .ToListAsync();
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

    }
}

