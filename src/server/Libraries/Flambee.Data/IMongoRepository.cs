using Flambee.Core;
using Flambee.Core.Helper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Flambee.Data
{
    public interface IMongoRepository<TEntity> where TEntity : IBaseEntity
    {
        Task<TEntity> GetById(object id);
        Task<IList<TEntity>> GetByIds(IList<object> ids);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);
        Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        Task<IPagedList<TEntity>> GetAllPaged(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<TEntity> Insert(TEntity entity);
        Task<IList<TEntity>> Insert(IList<TEntity> entities);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Update(TEntity entity, Expression<Func<TEntity, bool>> predicate = null);
        Task Delete(Expression<Func<TEntity, bool>> predicate);
        Task<int> DeleteMany(Expression<Func<TEntity, bool>> predicate);
        IMongoQueryable<TEntity> Query { get; }
        IMongoCollection<TEntity> Collection { get; }
    }
}
