
using HelloParent.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentBySchoolId(string id);    
    }
}
