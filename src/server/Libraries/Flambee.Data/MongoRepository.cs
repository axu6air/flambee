using Flambee.Core;
using Flambee.Core.Helper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDbGenericRepository.Utils;

namespace Flambee.Data
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : IBaseEntity
    {
        private readonly IMongoCollection<TEntity> _context;

        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            //_context = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

            _context = database.GetCollection<TEntity>(typeof(TEntity).Name.Pluralize());
        }


        public async Task<TEntity> GetById(object id)
        {
            //var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);
            return await _context.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IList<TEntity>> GetByIds(IList<object> ids)
        {
            //return await _context.Set<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync();
            var list = await _context.FindAsync(x => ids.Contains(x.Id));
            return list.ToList();
        }
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            try
            {
                var res = await _context.Find(predicate).FirstOrDefaultAsync();
                return (TEntity)res;

            } catch (Exception ex)
            {
            }

            return default;
        }

        public async Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var list = await _context.FindAsync(predicate);
            return list.ToList();
        }

        public async Task<IPagedList<TEntity>> GetAllPaged(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            IQueryable<TEntity> query = _context.AsQueryable();

            query = func != null ? await func(query) : query;

            return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                await _context.InsertOneAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                return default;
            }

        }

        public async Task<IList<TEntity>> Insert(IList<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await _context.InsertManyAsync(entities);

            return entities;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            return await _context.FindOneAndReplaceAsync(filter, entity);
        }

        public async Task<TEntity> Update(TEntity entity, Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _context.FindOneAndReplaceAsync<TEntity>(predicate, entity);
        }

        public async Task Delete(Expression<Func<TEntity, bool>> predicate)
        {
            await _context.FindOneAndDeleteAsync(predicate);
        }

        public async Task<int> DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var deleteResult = await _context.DeleteManyAsync(predicate);
            return (int)deleteResult.DeletedCount;
        }

        public IMongoQueryable<TEntity> Query
        {
            get
            {
                return _context.AsQueryable<TEntity>();
            }
        }

        public IMongoCollection<TEntity> Collection
        {
            get
            {
                return _context;
            }
        }

        private protected static string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

    }
}
