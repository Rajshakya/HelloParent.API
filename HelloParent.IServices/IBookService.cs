
using HelloParent.Entities.LMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices
{
    public interface IBookService
    {
        Task<bool> AddBook(Book responses);       
        Task<bool> UpdateBook(Book responses);
        Task<bool> DeleteBook(Book responses);
        Task<List<Book>> GetBooks(string schoolId);
        Task<Book> GetBook(int schoolId);
    }
}
