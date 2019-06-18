using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloParent.Base.Repository.Interfaces;
using HelloParent.Entities.LMS;
using HelloParent.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAPS.Kidzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTransactionController : ControllerBase
    {

        private readonly IMapperService _mapperService;
        private readonly IBookTransactionRepository _bookTransactionRepository;

        public BookTransactionController(IMapperService mapperService, IBookTransactionRepository bookTransactionRepository)
        {
            _mapperService = mapperService;
            _bookTransactionRepository = bookTransactionRepository;
        }
        [HttpGet("getTranscationsbook")]
        public async Task<IActionResult> GetTranscationsBook()
        {
           return Ok();
        }

        /// <summary>
        /// Issue Book
        /// </summary>
        /// <param name="bookTransaction"></param>
        /// <returns>boolean</returns>
        [HttpPost("bookissue")]
        public async Task<IActionResult> IssueBook([FromBody] List<BookTranscation> books)
        {
            try
            {
                foreach(BookTranscation book in books)
                {
                    var result = await _bookTransactionRepository.IssueBook(book);
                }
                return Ok(true);
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
        /// Return Book
        /// </summary>
        /// <param name="bookTransaction"></param>
        /// <returns>boolean</returns>
        [HttpPost]
        [Route("~/api/bookTransaction/ReturnBook")]
        public async Task<IActionResult> ReturnBook([FromBody] List<BookTranscation> books)
        {
            try
            {
                foreach (BookTranscation book in books)
                {
                    var result = await _bookTransactionRepository.ReturnBook(book);
                }
                return Ok(true);
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