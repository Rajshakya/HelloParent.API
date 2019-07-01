using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using HelloParent.IServices;
using HelloParent.MongoBase.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HelloParent.Services
{
   public class FeeService : MongoBaseService<Fee>, IFeeService
    {
        public FeeService(IRepository<Fee> repository) : base(repository)
        {

        }

        public Task CreateAndSendInvoice(bool sendNotification, Transaction transaction, School school, ApplicationUser userWithFeeRight)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTransactionFromFees(ObjectId feeId, ObjectId transactionId, School school)
        {
            throw new NotImplementedException();
        }

        public Task<Fee> GenerateFee(Student student, FeeCycle cycle, School school)
        {
            throw new NotImplementedException();
        }

        public Task GenerateFeeCycles(ObjectId sessionId, ObjectId schoolId)
        {
            throw new NotImplementedException();
        }

        public string GenerateInvoicePdf(ObjectId transactionId)
        {
            throw new NotImplementedException();
        }

        public string GeneratePdf(ObjectId transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<Fee> GetById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Fee>> GetFeeByExpression(IEnumerable<Expression<Func<Fee, bool>>> filter)
        {
           return await Get(filter);
        }

        public Task UpdateFeeStatus(IEnumerable<ObjectId> feeIds, bool toNotify, FeeStatus toUpdate, ApplicationUser user, string remark = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLateFeesIfChanged(ObjectId feeId, double amount, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentFee(IDictionary<ObjectId, double> components, ObjectId feeId, School school, FeeStatus feeStatus, string remark = null)
        {
            throw new NotImplementedException();
        }
    }
}
