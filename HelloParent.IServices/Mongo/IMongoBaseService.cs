using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices.Mongo
{
    /// <summary>
    /// Generic  repository contract for mongo db database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMongoBaseService<T> where T : ITrackable
    {
        // TODO: comment get
        Task<List<T>> Get(Expression<Func<T, bool>> filter);
        // TODO: comment add
        Task<T> Add(T obj);
        // TODO: comment update
        Task<T> Update(T obj);
        // TODO: comment delete
        Task<bool> Delete(T obj);
        // TODO: addMany
        Task<IEnumerable<T>> AddMany(IEnumerable<T> obj);
        // TODO: get with flter
        Task<List<T>> Get(IEnumerable<Expression<Func<T, bool>>> filter);

        Task<List<BsonDocument>> Get(IEnumerable<Expression<Func<T, bool>>> filter,
            params Expression<Func<T, object>>[] projections);

        Task<List<T>> Get(IEnumerable<Expression<Func<T, bool>>> filter, Paging paging,
            Expression<Func<T, object>> sortBy);

        Task<List<BsonDocument>> Get(Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] projections);

        Task<List<T>> GetIncludingDeleted(IEnumerable<Expression<Func<T, bool>>> filter);
        Task<Dictionary<ObjectId, string>> GetNamesOnly(Expression<Func<T, bool>> filter);
        Task<long> Count(IEnumerable<Expression<Func<T, bool>>> filter);
    }
}
