using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPS.Kidzo.API.Views
{
    public class IssuedBookView
    {
        public string StudentId { get; set; }
        public string StudentName { get; set;}
        public DateTime IssuedDate { get; set; }
    }
}
