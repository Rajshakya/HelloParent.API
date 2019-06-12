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
    public class MongoRepository<T> : IRepository<T> where T : ITrackable, IEntity<ObjectId>
    {
        public IMongoCollection<T> Collection => throw new NotImplementedException();

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> AddManyAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> FindManyAndSet<TField>(Expression<Func<T, bool>> query, Expression<Func<T, TField>> fieldExpression, TField value)
        {
            throw new NotImplementedException();
        }

        public IAggregateFluent<T> GetAggregator(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IAggregateFluent<T> GetAggregator(Expression<Func<T, bool>> expression, int batchSize)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAsync(IEnumerable<Expression<Func<T, bool>>> filters, Paging paging, Expression<Func<T, object>> descendingSortField)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAsyncByFilter(FilterDefinition<T> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync(IEnumerable<Expression<Func<T, bool>>> filters)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetIncludingDeletedAsync(IEnumerable<Expression<Func<T, bool>>> filters, Paging paging, Expression<Func<T, object>> descendingSortField)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<ObjectId, string>> GetNamesOnlysAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<ObjectId>> GetOnlyIdsAsync(Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HardDeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
