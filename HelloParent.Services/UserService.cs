using HelloParent.Entities.Model;
using HelloParent.IServices;
using HelloParent.MongoBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Services
{
    public class UserService : MongoBaseService<ApplicationUser>, IUserService
    {
        public UserService(IRepository<ApplicationUser> repository) : base(repository)
        {

        }
        public async Task<ApplicationUser> GetById(string id)
        {
            var data = await Get(x => x.Id == id);
            var obj = data.FirstOrDefault();
            return obj;
        }
    }
}
