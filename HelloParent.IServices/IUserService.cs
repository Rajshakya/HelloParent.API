using HelloParent.Entities.Enums;
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
   public interface IUserService : IMongoBaseService<ApplicationUser>
    {
       
        Task<ApplicationUser> GetById(string id);
    }
}
