using HelloParent.Entities;
using HelloParent.Entities.Model;
using HelloParent.IServices;
using HelloParent.MockData;
using HelloParent.MongoBase.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Services
{
    public class StudentService : MongoBaseService<Student>, IStudentService
    {
        public StudentService(IRepository<Student> repository):base(repository)
        {

        }
        public async Task<List<Student>> GetStudentBySchoolId(string id)
        {
            return await Get(x => x.SchoolId == ObjectId.Parse(id) && x.DeletedAt==null);
        }
    }
}
