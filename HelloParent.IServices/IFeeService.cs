using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using HelloParent.IServices.Mongo;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace HelloParent.IServices
{
    public interface IFeeService : IMongoBaseService<Fee>
    {
        Task GenerateFeeCycles(ObjectId sessionId, ObjectId schoolId);
        Task<Fee> GenerateFee(Student student, FeeCycle cycle, School school);
        string GeneratePdf(ObjectId transactionId);

        Task UpdateFeeStatus(IEnumerable<ObjectId> feeIds, bool toNotify, FeeStatus toUpdate, ApplicationUser user,
            string remark = null);

        Task UpdateStudentFee(IDictionary<ObjectId, double> components, ObjectId feeId, School school, FeeStatus feeStatus,
            string remark = null);

        string GenerateInvoicePdf(ObjectId transactionId);
        Task UpdateLateFeesIfChanged(ObjectId feeId, double amount, DateTime date);

        Task CreateAndSendInvoice(bool sendNotification, Transaction transaction, School school,
            ApplicationUser userWithFeeRight);

        Task DeleteTransactionFromFees(ObjectId feeId, ObjectId transactionId, School school);
        Task<Fee> GetById(ObjectId id);
        Task<List<Fee>> GetFeeByExpression(IEnumerable<Expression<Func<Fee,bool>>> filter);
    }
}
