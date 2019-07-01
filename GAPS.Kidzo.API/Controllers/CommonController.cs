using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloParent.Entities.Enums;
using HelloParent.Utilities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAPS.Kidzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        public CommonController()
        {

        }
        [HttpGet("feefrequency")]
        public async Task<IActionResult> GetFeeFrequency()
        {
            try
            {
                var studentFeeFrequency = EnumHelper.GetEnumLabels(typeof(StudentFeeFrequency)).ToList();
                studentFeeFrequency.Insert(0, new KeyValuePair<string, string>("All", "All"));
                var enums = studentFeeFrequency.ToDictionary(x => x.Key,
                    x => x.Value);
                return Ok(enums);
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
        [HttpGet("feestatus")]
        public async Task<IActionResult> FeeStatus()
        {
            try
            {
                var feeStatus = EnumHelper.GetEnumLabels(typeof(FeeStatus)).ToList();
                feeStatus.Insert(0, new KeyValuePair<string, string>("All", "All"));
                var enums = feeStatus.ToDictionary(x => x.Key,
                      x => x.Value);
                return Ok(enums);
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