using HelloParent.Entities.LMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Base.Repository.Interfaces
{
    public interface IBookTransactionRepository
    {
        Task<bool> IssueBook(BookTranscation book);
        Task<bool> ReturnBook(BookTranscation book);
        Task<IEnumerable<IssuedBook>> GetIssuedBooksByStudId(string studentId);
        Task<BookTranscation> GetBookTranscationByBookId(long Id);

    }
}
