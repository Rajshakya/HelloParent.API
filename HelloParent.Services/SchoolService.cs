using HelloParent.Entities.Model;
using HelloParent.IServices;
using HelloParent.MongoBase.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Services
{

    /// <summary>
    /// School service 
    /// </summary>
    public class SchoolService : MongoBaseService<School>, ISchoolService
    {
        public SchoolService(IRepository<School> repository) : base(repository)
        {

        }

        public async Task<School> GetSchoolById(string id)
        {
            var school = await Get(x => x.Id == ObjectId.Parse(id) && x.DeletedAt == null);
            if (school != null && school.Count > 0)
            {
                return school.First();
            }
            return null;
        }
       
    }
}
