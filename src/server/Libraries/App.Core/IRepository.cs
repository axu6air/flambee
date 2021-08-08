using App.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<IList<TEntity>> GetByIdsAsync(IList<object> ids);
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<IList<TEntity>> InsertAsync(IList<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IList<TEntity>> UpdateAsync(IList<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> ExecuteStoredProcedureAsync(string procedureName, params object[] parameters);
        Task<List<object>> ExecutedProcedureObjectAsync(string procedureName, params object[] parameters);
    }
}
