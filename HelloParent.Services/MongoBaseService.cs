using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using HelloParent.IServices.Mongo;
using HelloParent.MongoBase.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Services
{
    public abstract class MongoBaseService<T> : IMongoBaseService<T> where T : ITrackable
    {
        protected readonly IRepository<T> Repository;


        public MongoBaseService(IRepository<T> repository)
        {
            Repository = repository;
        }

        public async Task<Dictionary<ObjectId, string>> GetNamesOnly(Expression<Func<T, bool>> filter)
        {
            return await Repository.GetNamesOnlysAsync(filter);
        }
        public async Task<List<T>> Get(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = x => true;
            }
            return await Get(new[] { filter });
        }

        public async Task<List<T>> Get(IEnumerable<Expression<Func<T, bool>>> filter)
        {

            return await Repository.GetAsync(filter, null, null);

        }
        public async Task<long> Count(IEnumerable<Expression<Func<T, bool>>> filter)
        {

            return await Repository.GetCountAsync(filter);

        }
        public async Task<List<T>> Get(IEnumerable<Expression<Func<T, bool>>> filter, Paging paging, Expression<Func<T, object>> sortBy)
        {

            return await Repository.GetAsync(filter, paging, sortBy);

        }
        public async Task<List<T>> GetIncludingDeleted(IEnumerable<Expression<Func<T, bool>>> filter)
        {

            return await Repository.GetIncludingDeletedAsync(filter, null, null);

        }

        public async Task<List<BsonDocument>> Get(IEnumerable<Expression<Func<T, bool>>> filter,
           params Expression<Func<T, object>>[] projections)
        {
            if (filter == null)
            {
                filter = new List<Expression<Func<T, bool>>> { };
            }

            var filters = filter.ToList();
            filters.Add(x => x.DeletedAt == null);


            var builder = filters.Aggregate(Builders<T>.Filter.Empty,
                (current, expression) => current & Builders<T>.Filter.Where(expression));

            var list = projections.Select(x => Builders<T>.Projection.Include(x));

            var bson = Builders<T>.Projection.Combine(list);

            try
            {
                return
                    await
                        Repository.Collection.Aggregate()
                            .Match(builder)
                            .Project(bson)
                            .ToListAsync();
            }
            catch (MongoConnectionException)
            {
                return new List<BsonDocument>();
            }
        }
        public async Task<List<BsonDocument>> Get(Expression<Func<T, bool>> filter,
           params Expression<Func<T, object>>[] projections)
        {
            return await Get(new List<Expression<Func<T, bool>>> { filter }, projections);
        }

        public virtual async Task<T> Add(T obj)
        {
            return await Repository.AddAsync(obj);
        }

        public async Task<IEnumerable<T>> AddMany(IEnumerable<T> obj)
        {
            return await Repository.AddManyAsync(obj);
        }

        public async Task<T> Update(T obj)
        {
            return await Repository.UpdateAsync(obj);
        }

        public virtual async Task<bool> Delete(T obj)
        {
            return await Repository.DeleteAsync(obj);
        }
    }
}
