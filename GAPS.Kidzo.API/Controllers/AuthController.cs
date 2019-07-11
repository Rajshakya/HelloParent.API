using HelloParent.Entities.Model;
using HelloParent.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseAnnonymousController
    {
        private readonly IUserService _UserService;
        private readonly ISchoolService _SchoolService;
        private readonly IConfiguration _Configuration;
        public AuthController(IUserService userService, ISchoolService schoolService, IConfiguration configuration)
        {
            _UserService = userService;
            _SchoolService = schoolService;
            _Configuration = configuration;
        }
        [HttpGet]
        [Route("IsLoggedIn")]
        public async Task<IActionResult> IsLoggedIn()
        {

            string token = string.Empty;

            var v2LoginHeaderValue = Request.Headers["v2Login"];

            if (string.IsNullOrWhiteSpace(Convert.ToString(v2LoginHeaderValue)))
            {
                return Unauthorized();
            }


            string userId = Base64Decode(v2LoginHeaderValue);

            var user = (await _UserService.Get(x => x.Id == userId)).FirstOrDefault();

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>();

            user.Roles.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
           
            var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(user.Rights);
            claims.Add(new Claim("Rights", jsonObj));

            var school = await _SchoolService.GetSchoolById(user.SchoolId.ToString());
            claims.Add(new Claim(ClaimTypes.PrimarySid, school.Id.ToString()));
            if (school.SuperAdmins.Contains(user.Id))
            {
                claims.Add(new Claim(ClaimTypes.Role, RoleNames.SchoolAdmin));
              
            }

            GenericIdentity identity = new GenericIdentity("userid", userId);
            //JwtSecurityToken tokenOptions = new JwtSecurityToken(claims: claims);

            //

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:2000",
                audience: "http://localhost:2000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { token = token });
        }

       
    }


}