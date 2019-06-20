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
    public class BookTransactionRepository : IBookTransactionRepository
    {
        private readonly string _connectionString;
        private IDbConnection _connection { get { return new SqlConnection(_connectionString); } }

        public BookTransactionRepository()
        {
            _connectionString = AppSettings.SQLConnestionString;
        }


        public async Task<bool> IssueBook(BookTranscation book)
        {
            try
            {
                using (IDbConnection dbConnection = _connection)
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    #region Bind sql parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@StudentId", book.StudentId);
                    parameters.Add("@BookId", book.BookId);
                    parameters.Add("@IssueDate", DateTime.Now);
                    parameters.Add("@CreatedAt", DateTime.Now);
                    
                    #endregion

                    var result = await dbConnection.ExecuteAsync("Usp_issue_book", parameters, commandType: CommandType.StoredProcedure);
                    return true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ReturnBook(BookTranscation book)
        {
            try
            {
                using (IDbConnection dbConnection = _connection)
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    #region Bind sql parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", book.Id);
                    parameters.Add("@BookId", book.BookId);
                    parameters.Add("@ReturnDate", DateTime.Now);
                    parameters.Add("@UpdatedAt", DateTime.Now);

                    #endregion

                    var result = await dbConnection.ExecuteAsync("Usp_return_book", parameters, commandType: CommandType.StoredProcedure);
                    return true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<IssuedBook>> GetIssuedBooksByStudId(string studentId)
        {
            try
            {
                using (IDbConnection dbConnection = _connection)
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    #region Bind sql parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@StudentId", studentId);

                    #endregion
                    var result = await dbConnection.QueryAsync<IssuedBook>("Usp_GetIssuedBooks", parameters, commandType: CommandType.StoredProcedure);
                    return result;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BookTranscation> GetBookTranscationByBookId(long Id)
        {
            try
            {
                using (IDbConnection dbConnection = _connection)
                {
                    if (dbConnection.State == ConnectionState.Closed)
                        dbConnection.Open();

                    #region Bind sql parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@BookId", Id);

                    #endregion
                    var result = await dbConnection.QuerySingleAsync<BookTranscation>("Usp_GetIssuedBooksByBookId", parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
