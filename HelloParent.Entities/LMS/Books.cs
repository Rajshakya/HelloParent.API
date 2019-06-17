
using HelloParent.Entities.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.LMS
{
    public class Book: SQLBaseEntity
    {
        public string SchoolId { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime ? OrderDate { get; set; }
        public float ? Cost { get; set; }
        public string ShelfRackPosition { get; set; }
        public string VendorName { get; set; }
        public string YearOfPublication { get; set; }
        public string Subject { get; set; }
        public CategoryEnum Category { get; set; }
        public DateTime ? PurchaseDate { get; set; }
        public string ISBNNo { get; set; }
        public string DDC { get; set; }
        public string Keywords { get; set; }
        public int Pages { get; set; }
        public string Remarks { get; set; }

    }
}
