using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.LMS
{
    public class BookTranscation:SQLBaseEntity
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
