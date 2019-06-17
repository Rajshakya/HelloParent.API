
using HelloParent.Entities.LMS;
using HelloParent.IServices;
using HelloParent.MockData;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Services
{
    public class BookService : IBookService
    {
        public async Task<bool> AddBook(Book responses)
        {
            return true;
        }

        public async Task<bool> DeleteBook(Book responses)
        {
            return true;
        }

        public async Task<Book> GetBook(int id)
        {
           
            var result = new Book()
            {
                //Id = ObjectId.GenerateNewId(),
                //SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
                //Name = "For whom the Bell Tolls",
                //PublisherName = "",
                //AuthorName = "Ernest Hemingway",
                //Status = Entities.Enums.StatusEnum.Available,
                //ShelfRackPosition = "Middle Position",
                //Subject = "English",
                //Category = Entities.Enums.CategoryEnum.Book,
                //ISBNNo = "ISB No",
                //DDC = "DDC",
                //Keywords = "Keywords",
                //Pages = 1,
                //CreatedAt = DateTime.Now
            };
            return result;
        }

        public async Task<List<Book>> GetBooks(string schoolId)
        {
          var results=  new BookMock().GetMockData();
            return  results;
        }

        public async Task<bool> UpdateBook(Book responses)
        {
            if (responses.Id != null)
            {
                return true;
            }
            return false;
        }
    }
}
