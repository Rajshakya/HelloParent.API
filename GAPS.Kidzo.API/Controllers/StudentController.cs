using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloParent.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GAPS.Kidzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapperService _mapperService;
        public StudentController(IStudentService studentService, IMapperService mapperService)
        {
            _studentService = studentService;
            _mapperService = mapperService;
        }
       

        /// <summary>
        /// Get Students By School
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudents(string id)
        {
            try
            {
                var result = await _studentService.GetStudentBySchoolId("56e45c3af289df1048faced3");
                var mappedResult = _mapperService.MapStudentToStudentView(result);
                return Ok(mappedResult);
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