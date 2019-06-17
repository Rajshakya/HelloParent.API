using Dapper;
using HelloParent.Base.Repository.Interfaces;
using HelloParent.Entities.LMS;
using HelloParent.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Base.Repository
{
   public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;
        private IDbConnection _connection { get { return new SqlConnection(_connectionString); } }

        public BookRepository()
        {
            _connectionString = AppSettings.SQLConnestionString;
        }
        public async Task<long> AddBook(Book book)
        {
            try
            {
                using (IDbConnection dbConnection = _connection)
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    #region Bind sql parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@SchoolId", book.SchoolId);
                    parameters.Add("@Name", book.Name);
                    parameters.Add("@AuthorName", book.AuthorName);
                    parameters.Add("@PublisherName", book.PublisherName);
                    parameters.Add("@Status", book.Status);
                    parameters.Add("@OrderDate", book.OrderDate);
                    parameters.Add("@Cost", book.Cost);
                    parameters.Add("@ShelfRackPosition", book.ShelfRackPosition);
                    parameters.Add("@VendorName", book.VendorName);
                    parameters.Add("@YearOfPublication", book.YearOfPublication);
                    parameters.Add("@Subject", book.Subject);
                    parameters.Add("@Category", book.Category);
                    parameters.Add("@PurchaseDate", book.PurchaseDate);
                    parameters.Add("@ISBNo", book.ISBNNo);
                    parameters.Add("@DDC", book.DDC);
                    parameters.Add("@Keywords", book.Keywords);
                    parameters.Add("@Pages", book.Pages);
                    parameters.Add("@Remarks", book.Remarks);
                    parameters.Add("@CreatedAt", DateTime.Now);
                    parameters.Add("@UpdatedAt", book.UpdatedAt);
                    parameters.Add("@DeletedAt", book.DeletedAt);
                    #endregion

                    var result = await dbConnection.ExecuteAsync("Usp_add_book", parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public Task<long> DeleteBook(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBook(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBooks(string schoolId)
        {
           
                using (IDbConnection dbConnection = _connection)
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    #region Bind sql parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@SchoolId", schoolId);
                   
                    #endregion

                    var result = await dbConnection.QueryAsync<Book>("usp_GetBooksBySchoolId", parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            
        }

        public Task<long> UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
