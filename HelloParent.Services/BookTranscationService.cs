
using HelloParent.Entities.LMS;
using HelloParent.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Services
{
    public class BookTranscationService : IBookTranscationService
    {
        public  async Task<bool> BookIssue(Entities.LMS.BookTranscation responses)
        {
            return true;
        }

        public async Task<bool> BookReturn(Entities.LMS.BookTranscation responses)
        {
            return true;
        }


    }
}
