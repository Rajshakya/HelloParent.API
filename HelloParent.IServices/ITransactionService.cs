using HelloParent.Entities.Common;
using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using HelloParent.IServices.Mongo;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.IServices
{
    public interface ITransactionService : IMongoBaseService<Transaction>
    {
        Task SyncFeeForTransaction(Transaction transaction);
        Task UpdateTransactionStatus(Transaction model);
        Task SyncFeeForOnlinePayement(IEnumerable<TransactionItemClass> items, Transaction transaction);
        Task CollectStudentFee(Transaction domainModel, bool toNotify, ApplicationUser teacher);

        Task UpdateMultipleTransactionStatus(IEnumerable<ObjectId> transactionIds, bool toNotify,
            TransactionStatus newStatus, ApplicationUser user);

        Task SyncFeeForTransactionNew(Transaction transaction);
        Task DeleteTransactionForSchool(ObjectId transactionId, School school);

        Task CreateTransaction(TransactionCreateWithComponentViewModel model, Student student,
            School school, string userId);
    }
}
