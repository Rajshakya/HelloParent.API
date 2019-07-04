using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using HelloParent.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.MongoBase.Repository
{
   
    public class MongoRepository<T> : IRepository<T> where T : ITrackable, IEntity<ObjectId>
    {
        public IMongoCollection<T> Collection { get; private set; }
        protected IMongoDatabase Db { get; private set; }


      

        public MongoRepository()
        {
            var url = MongoUrl.Create(AppSettings.DbUrl);
            var setting = new MongoClientSettings
            {
                WaitQueueSize = 2000,
                Servers = url.Servers,
                MaxConnectionPoolSize = 1000,
                UseSsl = true
            };
            var cred = url.GetCredential();
            if (cred != null)
            {
                setting.Credential = cred;
            }
            MongoClient client = new MongoClient(setting);
            var dbName = AppSettings.DbEnv;

            Db = client.GetDatabase(dbName);
            Collection = Db.GetCollection<T>(typeof(T).Name.ToLower());
        }


        public void Add(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            Collection.InsertOneAsync(entity).Wait();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddManyAsync(IEnumerable<T> entities)
        {
            var enumerable = entities as IList<T> ?? entities.ToList();
            if (enumerable.Count == 0)
            {
                return enumerable;
            }
            enumerable.ToList().ForEach(x => {
                x.CreatedAt = DateTime.Now;
                x.UpdatedAt = DateTime.Now;
            });
            await Collection.InsertManyAsync(enumerable);
            return enumerable;
        }

        public void Delete(T entityToDelete)
        {
            entityToDelete.UpdatedAt = DateTime.Now;
            entityToDelete.DeletedAt = DateTime.Now;
            Update(entityToDelete);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.DeletedAt = DateTime.Now;
            var result = await UpdateAsync(entity);
            //     var result = await Collection.DeleteManyAsync(x => x.Id == entity.Id);
            return result.DeletedAt == null;
        }

        public async Task<bool> HardDeleteAsync(T entity)
        {
            var result = await Collection.DeleteManyAsync(x => x.Id == entity.Id);
            return result.DeletedCount > 0;
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return await Collection.Find(x => x.DeletedAt == null).ToListAsync();
            }
            try
            {
                return
                    await
                        Collection.Find(Builders<T>.Filter.And(
                            Builders<T>.Filter.Where(filter),
                            Builders<T>.Filter.Where(x => x.DeletedAt == null))).ToListAsync();
            }
            catch (MongoConnectionException)
            {
                return new List<T>();
            }
        }

        public async Task<List<T>> GetAsync(IEnumerable<Expression<Func<T, bool>>> filters, Paging paging,
            Expression<Func<T, object>> descendingSortField)
        {
            var expressions = filters.Select(filter => Builders<T>.Filter.Where(filter)).ToList();
            expressions.Add(Builders<T>.Filter.Where(x => x.DeletedAt == null));
            try
            {
                var filtered =
                    Collection.Find(Builders<T>.Filter.And(
                        expressions
                        ));
                if (paging != null)
                {
                    Expression<Func<T, object>> defaultSort = x => x.CreatedAt;

                    descendingSortField = descendingSortField ?? defaultSort;

                    filtered =
                        filtered.Sort(Builders<T>.Sort.Descending(descendingSortField))
                            .Skip((paging.Page - 1) * paging.Count)
                            .Limit(paging.Count);
                }
                return await filtered.ToListAsync();
            }
            catch (MongoConnectionException)
            {
                return new List<T>();
            }
        }

        public async Task<List<T>> GetIncludingDeletedAsync(IEnumerable<Expression<Func<T, bool>>> filters,
            Paging paging, Expression<Func<T, object>> descendingSortField)
        {
            var expressions = filters.Select(filter => Builders<T>.Filter.Where(filter)).ToList();
            //expressions.Add(Builders<T>.Filter.Where(x => x.DeletedAt == null));
            try
            {
                var filtered =
                    Collection.Find(Builders<T>.Filter.And(
                        expressions
                        ));
                if (paging != null)
                {
                    Expression<Func<T, object>> defaultSort = x => x.CreatedAt;

                    descendingSortField = descendingSortField ?? defaultSort;

                    filtered =
                        filtered.Sort(Builders<T>.Sort.Descending(descendingSortField))
                            .Skip((paging.Page - 1) * paging.Count)
                            .Limit(paging.Count);
                }
                return await filtered.ToListAsync();
            }
            catch (MongoConnectionException)
            {
                return new List<T>();
            }
        }

        public async Task<long> GetCountAsync(IEnumerable<Expression<Func<T, bool>>> filters)
        {
            var expressions = filters.Select(filter => Builders<T>.Filter.Where(filter)).ToList();
            expressions.Add(Builders<T>.Filter.Where(x => x.DeletedAt == null));
            try
            {
                return
                    await
                        Collection.Find(Builders<T>.Filter.And(
                            expressions
                            )).CountAsync();
            }
            catch (MongoConnectionException)
            {
                return 0;
            }
        }

        public async Task<List<T>> GetAsyncByFilter(FilterDefinition<T> filter = null)
        {
            try
            {
                return
                    await
                        Collection.Find(Builders<T>.Filter.And(
                            Builders<T>.Filter.And(filter),
                            Builders<T>.Filter.Where(x => x.DeletedAt == null))).ToListAsync();
            }
            catch (MongoConnectionException)
            {
                return new List<T>();
            }
        }

        public async Task<List<ObjectId>> GetOnlyIdsAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return await Collection.Find(x => x.DeletedAt == null).Project(x => x.Id).ToListAsync();
            }
            try
            {
                return
                    await
                        Collection.Find(Builders<T>.Filter.And(
                            Builders<T>.Filter.Where(filter),
                            Builders<T>.Filter.Where(x => x.DeletedAt == null))).Project(x => x.Id).ToListAsync();
            }
            catch (MongoConnectionException)
            {
                return new List<ObjectId>();
            }
        }

        public async Task<Dictionary<ObjectId, string>> GetNamesOnlysAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                var toReturn =
                    await
                        Collection.Find(Builders<T>.Filter.And(
                            Builders<T>.Filter.Where(filter),
                            Builders<T>.Filter.Where(x => x.DeletedAt == null)))
                            .Sort(Builders<T>.Sort.Ascending("Name"))
                            .Project(Builders<T>.Projection.Include("Name"))
                            .ToListAsync();
                var dict = toReturn.ToDictionary(x => x["_id"].AsObjectId, x => x["Name"].AsString);
                return dict;
            }
            catch (MongoConnectionException)
            {
                return new Dictionary<ObjectId, string>();
            }
        }

        public void Update(T entityToUpdate)
        {
            entityToUpdate.UpdatedAt = DateTime.Now;
            Collection.FindOneAndReplace(Builders<T>.Filter.Where(x => x.Id == entityToUpdate.Id), entityToUpdate);
        }


        public async Task<T> UpdateAsync(T entityToUpdate)
        {
            entityToUpdate.UpdatedAt = DateTime.Now;
            var result =
                await
                    Collection.FindOneAndReplaceAsync(Builders<T>.Filter.Where(x => x.Id == entityToUpdate.Id),
                        entityToUpdate);
            return result;
        }
        public async Task<UpdateResult> FindManyAndSet<TField>(Expression<Func<T, bool>> query, Expression<Func<T, TField>> fieldExpression, TField value)
        {

            var result =
                await
                    Collection.UpdateManyAsync(Builders<T>.Filter.Where(query), Builders<T>.Update.Set(fieldExpression, value));
            return result;
        }


        public IAggregateFluent<T> GetAggregator(Expression<Func<T, bool>> expression)
        {
            var list = new List<Expression<Func<T, bool>>> {
                expression,
                x => x.DeletedAt == null
            };
            var builder = list.Aggregate(Builders<T>.Filter.Empty,
                (current, exp) => current & Builders<T>.Filter.Where(exp));
            return Collection.Aggregate().Match(builder);
        }
        public IAggregateFluent<T> GetAggregator(Expression<Func<T, bool>> expression, int batchSize)
        {
            var list = new List<Expression<Func<T, bool>>> {
                expression,
                x => x.DeletedAt == null
            };
            var option = new AggregateOptions();
            if (batchSize != 0)
            {
                option.BatchSize = batchSize;
            }
            var builder = list.Aggregate(Builders<T>.Filter.Empty,
                (current, exp) => current & Builders<T>.Filter.Where(exp));
            return Collection.Aggregate(option).Match(builder);
        }
    }
}
