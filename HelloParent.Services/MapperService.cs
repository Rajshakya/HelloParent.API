
using HelloParent.Entities.LMS;
using HelloParent.IServices;
using System;
using System.Collections.Generic;

namespace HelloParent.Services
{
    public class MapperService : IMapperService
    {
        public IList<BookViews> MapBookToBookView(IList<Book> books)
        {
            List<BookViews> bookViews = new List<BookViews>();
            foreach(var book in books)
            {
                var modal = new BookViews()
                {
                    Id = book.Id.ToString(),
                    CreatedAt = book.CreatedAt,
                    UpdatedAt = book.UpdatedAt,
                    DeletedAt = book.DeletedAt,
                    SchoolId = book.SchoolId.ToString(),
                    Name = book.Name,
                    AuthorName = book.AuthorName,
                    PublisherName = book.PublisherName,
                    Status = book.Status,
                    OrderDate = book.OrderDate,
                    Cost = book.Cost,
                    ShelfRackPosition = book.ShelfRackPosition,
                    VendorName = book.VendorName,
                    YearOfPublication = book.YearOfPublication,
                    Subject = book.Subject,
                    Category = book.Category,
                    PurchaseDate = book.PurchaseDate,
                    ISBNNo = book.ISBNNo,
                    DDC = book.DDC,
                    Keywords = book.Keywords,
                    Pages = book.Pages,
                    Remarks = book.Remarks,
                };
                if(book.Status==Entities.Enums.StatusEnum.Issued)
                {
                    modal.IssuedTo = "Raj";
                    modal.IssuedOn = DateTime.Now;
                }
                bookViews.Add(modal);
            }
            return bookViews;
        }
    }
}
