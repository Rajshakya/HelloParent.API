
using HelloParent.Entities.LMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices
{
    public interface IMapperService
    {
        IList<BookViews> MapBookToBookView(IList<Book> books);
    }
}
