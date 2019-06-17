using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloParent.Base.Repository.Interfaces;
using HelloParent.Entities.LMS;
using HelloParent.IServices;
using HelloParent.Utilities.Constants;
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
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookService bookService, IBookTranscationService bookTranscationService,IMapperService mapperService,
            IBookRepository bookRepository)
        {
            _bookService = bookService;
            _bookTranscationService = bookTranscationService;
            _mapperService = mapperService;
            _bookRepository = bookRepository;
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
                id = Constants.TestingSchool_Id;
                var result =await _bookRepository.GetBooks(id);
                var mappedResult = _mapperService.MapBookToBookView(result.ToList());
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
                return StatusCode(500, ex.Message);
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
                for (int i = 0; i < quantity; i++)
                {
                    book.SchoolId = Constants.TestingSchool_Id;
                    var result = await _bookRepository.AddBook(book);
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