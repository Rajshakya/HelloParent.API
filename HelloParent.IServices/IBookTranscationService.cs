
using HelloParent.Entities.LMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices
{
    public interface IBookTranscationService
    {
        Task<bool> BookIssue(BookTranscation responses);
        Task<bool> BookReturn(BookTranscation responses);
    }
}
