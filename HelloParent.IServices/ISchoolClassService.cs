using HelloParent.Entities.Model;
using HelloParent.IServices.Mongo;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices
{
    public interface ISchoolClassService : IMongoBaseService<SchoolClass>
    {
        Task<List<SchoolClass>> GetSchoolClassBySessionAndSchool(string sessionId,string schoolId);

        /// <summary>
        /// Returns class of school for given class Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SchoolClass> GetById(ObjectId id);

    }
}
