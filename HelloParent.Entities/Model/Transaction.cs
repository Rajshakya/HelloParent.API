using HelloParent.Entities.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


namespace HelloParent.Entities.Model
{
    public class Transaction : BaseEntity
    {
        public Transaction()
        {
            Items = new TransactionItemClass[] { };
            Fees = new FeeForTransactionModel[] { };
        }
        public ObjectId? StudentId { get; set; }
        public ObjectId? SchoolId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public AmountMode AmountMode { get; set; }
        public string BankName { get; set; }
        public string CreatedBy { get; set; }
        public string ChequeNumber { get; set; }
        public string TransactionNumber { get; set; }
        public TransactionItemClass[] Items { get; set; }
        public FeeForTransactionModel[] Fees { get; set; }
        public string Remark { get; set; }

        public string Identifier
        {
            get
            {
                var timeStamp = Id.Timestamp;
                return timeStamp.ToString();
            }
        }
    }

    public class FeeForTransactionModel
    {
        public ObjectId FeeId { get; set; }
        public double Amount { get; set; }
    }



    public class TransactionItemClass
    {
        public TransactionItemClass()
        {
            Components = new Collection<Component>();
        }
        public ObjectId Id { get; set; }
        public TransactionItemType ItemType { get; set; }
        public ObjectId ItemId { get; set; }
        public double ItemAmount { get; set; }
        public ICollection<Component> Components { get; set; }

    }
}
