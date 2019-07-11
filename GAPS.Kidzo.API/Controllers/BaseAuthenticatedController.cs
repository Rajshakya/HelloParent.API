using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HelloParent.Entities.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace HelloParent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseAuthenticatedController : ControllerBase
    {

        /// <summary>
        /// Returns id of logged user for which user belongs
        /// </summary>
        /// <returns></returns>
        public ObjectId GetLoggedUserId()
        {
            var user = User.Identity as ClaimsIdentity;
            if (user != null )
            {
                return ObjectId.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            return ObjectId.Empty;
        }

        /// <summary>
        /// Returns id of school user for which user belongs
        /// </summary>
        /// <returns></returns>
        public ObjectId GetMySchoolId()
        {
            var user = User.Identity as ClaimsIdentity;
            if (user != null)
            {
                return ObjectId.Parse(user.FindFirst(ClaimTypes.PrimarySid).Value);
            }
            return ObjectId.Empty;
        }

        /// <summary>
        /// Returns list of rights that given user has
        /// </summary>
        /// <returns></returns>
        //public UserRights GetMyRights()
        //{
        //    var user = User.Identity as ClaimsIdentity;

        //    if (user != null && user.HasClaim(x => x.Type == "Rights"))
        //    {
        //        var data = user.Claims.Where(x => x.Type == "Rights").Select(x => x.Value).FirstOrDefault();
        //        if (data != null)
        //        {
        //            try
        //            {
        //                var list = JsonConvert.DeserializeObject<UserRights>(data);


        //                return list;
        //            }
        //            catch (Exception)
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //    return null;
        //}

    }
}