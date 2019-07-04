
using HelloParent.Entities;
using HelloParent.Entities.Model;
using HelloParent.IServices.Mongo;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices
{
    /// <summary>
    /// Student service contract 
    /// </summary>
    public interface IStudentService: IMongoBaseService<Student>
    {
        Task<List<Student>> GetStudentBySchoolId(string id);
        Task<Student> GetStudentByStudentId(string id);
        Task<List<Student>> GetStudentByClass(string classId,string schoolId);
    }
}
