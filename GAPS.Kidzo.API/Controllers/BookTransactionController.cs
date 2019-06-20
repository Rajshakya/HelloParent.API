using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAPS.Kidzo.API.Views;
using HelloParent.Base.Repository.Interfaces;
using HelloParent.Entities.LMS;
using HelloParent.Entities.Model;
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
        private readonly IStudentService _studentService;

        public BookTransactionController(IMapperService mapperService, IBookTransactionRepository bookTransactionRepository, IStudentService studentService)
        {
            _mapperService = mapperService;
            _bookTransactionRepository = bookTransactionRepository;
            _studentService = studentService;
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
        [HttpPost("bookreturn")]
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


        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetIssuedBooks(string studentId)
        {
            try
            {
                var result = await _bookTransactionRepository.GetIssuedBooksByStudId(studentId);
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("~/api/BookTransaction/GetStudent/{bookId}")]
        public async Task<IActionResult> GetIssuedBookStudent(long bookId)
        {
            try
            {
                IssuedBookView issuedBookView = new IssuedBookView();
                var result = await _bookTransactionRepository.GetBookTranscationByBookId(bookId);
                if (result != null)
                {
                    var student = await _studentService.GetStudentByStudentId(result.StudentId);
                    issuedBookView.StudentId = student.Id.ToString();
                    issuedBookView.StudentName = student.Name;
                    issuedBookView.IssuedDate = result.IssueDate;
                }

                return Ok(issuedBookView);
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
