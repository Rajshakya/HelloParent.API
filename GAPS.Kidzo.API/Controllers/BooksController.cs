using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloParent.Entities.LMS;
using HelloParent.IServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAPS.Kidzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBookTranscationService _bookTranscationService;
        private readonly IMapperService _mapperService;

        public BooksController(IBookService bookService, IBookTranscationService bookTranscationService,IMapperService mapperService)
        {
            _bookService = bookService;
            _bookTranscationService = bookTranscationService;
            _mapperService = mapperService;
        }


        /// <summary>
        /// Get Books by school
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns>book list</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooks(string id)
        {
            try
            {
                var result =await _bookService.GetBooks(id);
                var mappedResult = _mapperService.MapBookToBookView(result);
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

        /// <summary>
        /// Add Book
        /// </summary>
        /// <param name="book"></param>
        /// <returns>boolean</returns>
        [HttpPost]
        [Route("~/api/books/{quantity}")]
        public async Task<IActionResult> AddBook([FromBody] Book book,int quantity)
        {
            try
            {
                var result =await _bookService.AddBook(book);
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

        /// <summary>
        /// Update Book
        /// </summary>
        /// <param name="book"></param>
        /// <returns>boolean</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            try
            {
                var result =await _bookService.UpdateBook(book);
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

        /// <summary>
        /// Book Issue
        /// </summary>
        /// <param name="bookTranscation"></param>
        /// <returns>boolean</returns>
        [HttpPost]
        public async Task<IActionResult> BookIssue([FromBody] BookTranscation bookTranscation)
        {
            try
            {
                var result =await _bookTranscationService.BookIssue(bookTranscation);
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

        /// <summary>
        /// Book Return
        /// </summary>
        /// <param name="bookTranscation"></param>
        /// <returns>boolean</returns>
        [HttpPost]
        public async Task<IActionResult> BookReturn([FromBody] BookTranscation bookTranscation)
        {
            try
            {
                var result =await _bookTranscationService.BookReturn(bookTranscation);
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