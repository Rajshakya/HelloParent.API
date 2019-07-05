using HelloParent.Auth;
using HelloParent.Base.Repository.Interfaces;
using HelloParent.Entities.LMS;
using HelloParent.IServices;
using HelloParent.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloParent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseAuthenticatedController
    {
        private readonly IBookService _bookService;
        private readonly IMapperService _mapperService;
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookService bookService, IMapperService mapperService,
            IBookRepository bookRepository)
        {
            _bookService = bookService;
            _mapperService = mapperService;
            _bookRepository = bookRepository;
        }


        /// <summary>
        /// Get Books by school
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns>book list</returns>
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                string id = Constants.TestingSchool_Id;
                IEnumerable<Book> result = await _bookRepository.GetBooks(id);
                IList<BookViews> mappedResult = _mapperService.MapBookToBookView(result.ToList());
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
        public async Task<IActionResult> AddBook([FromBody] Book book, int quantity)
        {
            try
            {
                for (int i = 0; i < quantity; i++)
                {
                    book.SchoolId = Constants.TestingSchool_Id;
                    long result = await _bookRepository.AddBook(book);
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
                bool result = await _bookService.UpdateBook(book);
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