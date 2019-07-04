using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloParent.IServices;
using HelloParent.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAPS.Kidzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolClassController : ControllerBase
    {
        private readonly ISchoolClassService _schoolclassService;
        private readonly IMapperService _mapperService;
        public SchoolClassController(ISchoolClassService schoolclassService, IMapperService mapperService)
        {
            _schoolclassService = schoolclassService;
            _mapperService = mapperService;
        }

        /// <summary>
        /// Get School class By School Id and session id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{sessionId}/session")]
        public async Task<IActionResult> GetSchoolClassBySession(string sessionId)
        {
            var SchoolId = Constants.TestingSchool_Id;
            try
            {
                var result = await _schoolclassService.GetSchoolClassBySessionAndSchool(sessionId, SchoolId);
                return Ok(result);
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
                return StatusCode(500, ex);
            }
        }
    }
}