using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloParent.IServices;
using HelloParent.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloParent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseAuthenticatedController
    {

        private readonly IMapperService _mapperService;
        private readonly ISchoolService _schoolService;
        private readonly IUserService _userService;
        public UserController(IMapperService mapperService,ISchoolService schoolService,IUserService userService)
        {
            _mapperService = mapperService;
            _schoolService = schoolService;
            _userService = userService;
        }
        [HttpGet("LoggedUserInfo")]
        public async Task<IActionResult> GetLoggedUser()
        {
            try
            {
                LoggedUserInfo loggedUserInfo = new LoggedUserInfo();

                var school = await _schoolService.GetSchoolById(GetMySchoolId().ToString());
                var user =await _userService.GetById(GetLoggedUserId().ToString());
                loggedUserInfo.UserId = user.Id.ToString();
                loggedUserInfo.UserName = user.Name;
                loggedUserInfo.Mobile = user.PhoneNumber;
                loggedUserInfo.SchoolId = school.Id.ToString();
                loggedUserInfo.SchoolName = school.Name;
                return Ok(loggedUserInfo);
                 
            }
            catch (ArgumentNullException argNullEx)
            {
                return BadRequest(argNullEx.Message);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}