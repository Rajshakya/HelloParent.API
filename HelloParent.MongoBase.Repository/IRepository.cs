using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.MongoBase.Repository
{
    public interface IRepository<TEntity> where TEntity : ITrackable
    {

        IMongoCollection<TEntity> Collection { get; }
        Task<TEntity> UpdateAsync(TEntity entityToUpdate);
        Task<bool> DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities);

        Task<List<ObjectId>> GetOnlyIdsAsync(Expression<Func<TEntity, bool>> filter = null);
        IAggregateFluent<TEntity> GetAggregator(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> GetAsyncByFilter(FilterDefinition<TEntity> filter = null);
        Task<bool> HardDeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, Paging paging, Expression<Func<TEntity, object>> descendingSortField);
        Task<long> GetCountAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters);
        Task<List<TEntity>> GetIncludingDeletedAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, Paging paging, Expression<Func<TEntity, object>> descendingSortField);
        Task<Dictionary<ObjectId, string>> GetNamesOnlysAsync(Expression<Func<TEntity, bool>> filter);
        Task<UpdateResult> FindManyAndSet<TField>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TField>> fieldExpression, TField value);
        IAggregateFluent<TEntity> GetAggregator(Expression<Func<TEntity, bool>> expression, int batchSize);
    }
}
