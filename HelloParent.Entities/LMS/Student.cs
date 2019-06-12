using HelloParent.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities
{
    public class Student: BaseEntity
    {       
        public ObjectId StudentId { get; set; }
        public ObjectId SchoolId { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }       
        public string FatherName { get; set; }
        public string MotherName { get; set; }
    }
}
