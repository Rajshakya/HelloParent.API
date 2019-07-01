using HelloParent.Entities.Model;
using HelloParent.IServices;
using HelloParent.MongoBase.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Services
{
    public class SchoolClassService : MongoBaseService<SchoolClass>, ISchoolClassService
    {
        public SchoolClassService(IRepository<SchoolClass> repository) : base(repository)
        {

        }
        public async Task<List<SchoolClass>> GetSchoolClassBySessionAndSchool(string sessionId, string schoolId)
        {
            return await Get(x => x.SchoolId == ObjectId.Parse(schoolId) && x.SessionId == ObjectId.Parse(sessionId) && x.DeletedAt == null);
        }
       

    }
}
