using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloParent.Views
{
    public class LoggedUserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string SchoolId { get; set; }
        public string SchoolName { get; set; }
    }
}
