using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloParent.IServices;
using HelloParent.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GAPS.Kidzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        private readonly IMapperService _mapperService;
        public SchoolController(ISchoolService schoolService, IMapperService mapperService)
        {
            _schoolService = schoolService;
            _mapperService = mapperService;
        }

        /// <summary>
        /// Get Sessions By School Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("activeschool")]
        public async Task<IActionResult> GetActiveSchool()
        {
            try
            {
                var result = await _schoolService.GetSchoolById(Constants.TestingSchool_Id);
                return Ok(result.Sessions);
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

        /// <summary>
        /// Get Sessions By School Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("feecycles/{sessionId}")]
        public async Task<IActionResult> GetFeeCycles(string sessionId)
        {
            try
            {
                var result = await _schoolService.GetSchoolById(Constants.TestingSchool_Id);
                var feeCycles = result.FeeCycles.Where(x => x.SessionId == ObjectId.Parse(sessionId));
                return Ok(feeCycles);
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