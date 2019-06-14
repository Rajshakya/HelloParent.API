
using HelloParent.Entities.LMS;
using HelloParent.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices
{

    /// <summary>
    /// Mapper service to map db entity to views 
    /// </summary>
    public interface IMapperService
    {
        IList<BookViews> MapBookToBookView(IList<Book> books);
        IList<StudentViews> MapStudentToStudentView(IList<Student> students);
    }
}
