using HelloParent.Entities.LMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Base.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<long> AddBook(Book book);
        Task<long> UpdateBook(Book book);
        Task<long> DeleteBook(long id);
        Task<IEnumerable<Book>> GetBooks(string schoolId);
        Task<Book> GetBook(long id);
    }
}
