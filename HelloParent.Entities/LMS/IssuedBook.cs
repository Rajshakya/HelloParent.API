using HelloParent.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.LMS
{
    public class IssuedBook
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
