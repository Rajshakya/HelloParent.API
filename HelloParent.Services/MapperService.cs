
using HelloParent.Entities.LMS;
using HelloParent.Entities.Model;
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
                var model = new BookViews()
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
                    model.IssuedTo = "Raj";
                    model.IssuedOn = DateTime.Now;
                }
                bookViews.Add(model);
            }
            return bookViews;
        }

        public IList<StudentViews> MapStudentToStudentView(IList<Student> students)
        {
            List<StudentViews> studentViews = new List<StudentViews>();
            foreach(var student in students)
            {
                var model = new StudentViews()
                {
                    Id = student.Id.ToString(),
                    CreatedAt = student.CreatedAt,
                    UpdatedAt=student.UpdatedAt,
                    DeletedAt=student.DeletedAt,
                    SchoolId=student.SchoolId.ToString(),
                    Identifier=student.Identifier,
                    Name=student.Name,
                    FatherName=student.FatherName,
                    MotherName=student.MotherName
        
                };
                studentViews.Add(model);
            }
            return studentViews;
        }

        public FeeCycleSingleModel MapFeeCycleToFeeCycleSingleModel(FeeCycle feeCycle)
        {
            FeeCycleSingleModel feeCycleSingle = new FeeCycleSingleModel();
            feeCycleSingle.Id = feeCycle.Id;
            feeCycleSingle.Name = feeCycle.Name;
            feeCycleSingle.StartDate = feeCycle.StartDate;
            feeCycleSingle.EndDate = feeCycle.EndDate;
            feeCycleSingle.IdString = feeCycle.Id.ToString();
            feeCycleSingle.CreatedAt = feeCycle.CreatedAt;
            feeCycleSingle.LastDueDate = feeCycle.LastDueDate;
            feeCycleSingle.LateFee = feeCycle.LateFee;
            feeCycleSingle.SessionId = feeCycle.SessionId;
            return feeCycleSingle;
        }
    }
}
