using HelloParent.Entities.Common;
using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using HelloParent.IServices;
using HelloParent.MongoBase.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Services
{
    public class TransactionService : MongoBaseService<Transaction>, ITransactionService
    {
        public TransactionService(IRepository<Transaction> repository) : base(repository)
        {

        }

        public Task CollectStudentFee(Transaction domainModel, bool toNotify, ApplicationUser teacher)
        {
            throw new NotImplementedException();
        }

        public Task CreateTransaction(TransactionCreateWithComponentViewModel model, Student student, School school, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTransactionForSchool(ObjectId transactionId, School school)
        {
            throw new NotImplementedException();
        }

        public Task SyncFeeForOnlinePayement(IEnumerable<TransactionItemClass> items, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task SyncFeeForTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task SyncFeeForTransactionNew(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMultipleTransactionStatus(IEnumerable<ObjectId> transactionIds, bool toNotify, TransactionStatus newStatus, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTransactionStatus(Transaction model)
        {
            throw new NotImplementedException();
        }
    }
}
