﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.LMS
{
    public class BookTranscation:SQLBaseEntity
    {
        public string StudentId { get; set; }
        public long BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
