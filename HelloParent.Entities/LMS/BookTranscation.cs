using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.LMS
{
    public class BookTranscation:SQLBaseEntity
    {
        public ObjectId StudentId { get; set; }
        public ObjectId SchoolId { get; set; }
        public ObjectId BookId { get; set; }
        public DateTime DateOfIssue { get; set; }
        public ObjectId IssuedBy { get; set; }
        public DateTime ? DateOfReturn { get; set; }
        public ObjectId ? ReturnBy { get; set; }
        public string Remarks { get; set; }
    }
}
