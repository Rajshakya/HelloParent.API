using HelloParent.Entities.Model;
using HelloParent.IServices;
using HelloParent.MongoBase.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<SchoolClass> GetById(ObjectId id)
        {
            var data = await Get(x => x.Id == id);
            var obj = data.FirstOrDefault();
            return obj;
        }


    }
}
