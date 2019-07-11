using HelloParent.Entities.Model;
using HelloParent.IServices.Mongo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices
{
    /// <summary>
    /// School service contract 
    /// </summary>
    public interface ISchoolService : IMongoBaseService<School>
    {
        Task<School> GetSchoolById(string id);
       
    }
}
