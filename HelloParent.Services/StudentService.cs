using HelloParent.Entities;
using HelloParent.IServices;
using HelloParent.MockData;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Services
{
    public class StudentService : IStudentService
    {
        public async Task<List<Student>> GetStudentBySchoolId(string id)
        {
            var results = new StudentMock().GetMockData();
            return results;
        }
    }
}
