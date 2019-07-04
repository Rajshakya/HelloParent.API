using HelloParent.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloParent.Entities.Common
{
    public class TransactionCreateWithComponentViewModel
    {
        public IList<FeeModelForStudent> Fees { get; set; }
        public string StudentId { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public bool IsNotification { get; set; }
        public AmountMode AmountMode { get; set; }
        public string BankName { get; set; }
        public string Remark { get; set; }
        public string ChequeNumber { get; set; }
        public string TransactionNumber { get; set; }
    }
    public class FeeComponentModelTrans
    {
        public string ComponentId { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }
    }

    public class FeeModelForStudent
    {
        public FeeModelForStudent()
        {
            PayableComponents = new List<FeeComponentModelTrans>();
        }
        public string FeeCycleName { get; set; }
        public string SessionName { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
        public double TotalPayableAmount { get; set; }
        public double TotalPaid { get; set; }
        public double? LateFee { get; set; }
        public string Id { get; set; }
        public bool IsLateFeeValid { get; set; }

        public IList<FeeComponentModelTrans> PayableComponents { get; set; }

        public double GetFinalTotal()
        {
            var val = PayableComponents.Sum(x => x.Value);
            if (LateFee.HasValue)
            {
                val += LateFee.Value;
            }
            return val;
        }

    }
}
