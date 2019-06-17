
using HelloParent.Entities.LMS;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.MockData
{
    public class BookMock
    {
        public List<Book> GetMockData()
        {

            List<Book> books = new List<Book>();
            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "For whom the Bell Tolls",
            //    PublisherName = "",
            //    AuthorName = "Ernest Hemingway",
            //    Status = Entities.Enums.StatusEnum.Issued,
            //    ShelfRackPosition = "Middle Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Book,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC",
                
            //});
            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "Kurukku",
            //    PublisherName = "",
            //    AuthorName = "Faustina Bama",
            //    Status = Entities.Enums.StatusEnum.Available,
            //    ShelfRackPosition = "Middle Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Novel,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC"
            //});

            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "The Economic Times",
            //    PublisherName = "Bennet Collman and Company",
            //    AuthorName = "Bennet",
            //    Status = Entities.Enums.StatusEnum.Available,
            //    ShelfRackPosition = "Middle Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Newspaper,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC"
            //});

            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "Times of India",
            //    PublisherName = "Bennet Collman and Company",
            //    AuthorName = "Bennet",
            //    Status = Entities.Enums.StatusEnum.Available,
            //    ShelfRackPosition = "Middle Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Newspaper,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC"
            //});

            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "The Hindu",
            //    PublisherName = "Manoroma",
            //    AuthorName = "Manoroma",
            //    Status = Entities.Enums.StatusEnum.Available,
            //    ShelfRackPosition = "Middle Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Newspaper,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC"
            //});

            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "Nature",
            //    PublisherName = "Manoroma",
            //    AuthorName = "Manoroma",
            //    Status = Entities.Enums.StatusEnum.Available,
            //    ShelfRackPosition = "Middle Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Journal,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC"
            //});

            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "The Science Journal",
            //    PublisherName = "Manoroma",
            //    AuthorName = "Manoroma",
            //    Status = Entities.Enums.StatusEnum.Available,
            //    ShelfRackPosition = "Middle Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Journal,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC"
            //});

            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "Think and Grow Rich",
            //    PublisherName = "Scholastic India",
            //    AuthorName = "Jane G. Austin",
            //    Status = Entities.Enums.StatusEnum.Available,
            //    ShelfRackPosition = "Top Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Book,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC"
            //});

            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "Unlimited Power",
            //    PublisherName = "Ballantine Books",
            //    AuthorName = "Anthony Robbins",
            //    Status = Entities.Enums.StatusEnum.Available,
            //    ShelfRackPosition = "Top Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Book,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC"
            //});

            //books.Add(new Book()
            //{
            //    Id = ObjectId.GenerateNewId(),
            //    SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
            //    Name = "The Tale of Peter Rabbit",
            //    PublisherName = "FingerPrint Publishing",
            //    AuthorName = "Mohammed Awzal",
            //    Status = Entities.Enums.StatusEnum.Available,
            //    ShelfRackPosition = "Bottom Position",
            //    Subject = "English",
            //    Category = Entities.Enums.CategoryEnum.Book,
            //    ISBNNo = "ISB No",
            //    DDC = "DDC",
            //    Keywords = "Keywords",
            //    Pages = 1,
            //    CreatedAt = DateTime.Now,
            //    Remarks = "ABC"
            //});
            return books;
        }
    }
}
