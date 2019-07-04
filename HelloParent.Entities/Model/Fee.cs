using HelloParent.Entities.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HelloParent.Entities.Model
{
    public class Fee : BaseEntity
    {
        public Fee()
        {
            Components = new Component[] { };
            Transactions = new TransactionForFeeModel[] { };
        }

        public ObjectId StudentId { get; set; }
        public ObjectId FeeCycleId { get; set; }
        public ObjectId SchoolId { get; set; }
        public Component[] Components { get; set; }
        public FeeStatus FeeStatus { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string CancelledBy { get; set; }
        public DateTime? CancelledDate { get; set; }
        public double? LateFee { get; set; }
        public DateTime? LateFeeDate { get; set; }
        public string Remark { get; set; }
        public TransactionForFeeModel[] Transactions { get; set; }


        public double GetTotalLateFees(School school, DateTime date, FeeCycle feeCycle)
        {
            if (date.Date > feeCycle.LastDueDate.ToLocalTime().Date && GetTotalFees() > 0)
            {

                var totalLatefee = 0.0;
                switch (school.LateFeeType)
                {
                    case LateFeeType.Daily:
                        var days = DateTime.Today.Subtract(feeCycle.LastDueDate).Days;
                        totalLatefee = school.LateFeeAmount * days;
                        break;
                    case LateFeeType.Weekly:
                        var weeks = Convert.ToInt32(Math.Ceiling(((decimal)(DateTime.Today.Subtract(feeCycle.LastDueDate).Days)) / 7));
                        totalLatefee = school.LateFeeAmount * weeks;
                        break;
                    case LateFeeType.Monthly:
                        var month = Convert.ToInt32(Math.Ceiling(((decimal)(DateTime.Today.Subtract(feeCycle.LastDueDate).Days)) / 30));
                        totalLatefee = school.LateFeeAmount * month;
                        break;
                    default:
                        totalLatefee = school.LateFeeAmount;
                        break;
                }

                if (this.LateFee.HasValue)
                {
                    totalLatefee = this.LateFee.Value;
                }
                return totalLatefee;

            }
            return 0.0;
        }

        public double GetTotalLateFeesOfLateJoinedStudent(School school, DateTime date, FeeCycle feeCycle, DateTime joiningDate)
        {
            if (date.Date > joiningDate && GetTotalFees() > 0)
            {

                var totalLatefee = 0.0;
                switch (school.LateFeeType)
                {
                    case LateFeeType.Daily:
                        var days = DateTime.Today.Subtract(joiningDate).Days;
                        totalLatefee = school.LateFeeAmount * days;
                        break;
                    case LateFeeType.Weekly:
                        var weeks = Convert.ToInt32(Math.Ceiling(((decimal)(DateTime.Today.Subtract(joiningDate).Days)) / 7));
                        totalLatefee = school.LateFeeAmount * weeks;
                        break;
                    case LateFeeType.Monthly:
                        var month = Convert.ToInt32(Math.Ceiling(((decimal)(DateTime.Today.Subtract(joiningDate).Days)) / 30));
                        totalLatefee = school.LateFeeAmount * month;
                        break;
                    default:
                        totalLatefee = school.LateFeeAmount;
                        break;
                }

                if (this.LateFee.HasValue)
                {
                    totalLatefee = this.LateFee.Value;
                }
                return totalLatefee;

            }
            return 0.0;
        }

        public double GetTotalFees()
        {
            if (Components == null || Components.Length == 0)
            {
                return 0;
            }
            return Components.Sum(x => x.Value);
        }



        public double GetPendingFee(DateTime date, FeeCycle feeCycle, School school, DateTime? joiningDate)
        {
            var totalAmount = GetTotalFees();
            if (totalAmount <= 0)
            {
                return 0;
            }
            double totalLatefee = 0;
            if (date.Date > feeCycle.LastDueDate.ToLocalTime().Date && joiningDate > feeCycle.LastDueDate.ToLocalTime().Date)
            {
                //totalLatefee = GetTotalLateFees(school, date.Date, feeCycle);
                totalLatefee = Math.Round(GetTotalLateFeesOfLateJoinedStudent(school, date.Date, feeCycle, Convert.ToDateTime(joiningDate).ToLocalTime().Date), 2);
            }
            else
            {
                totalLatefee = GetTotalLateFees(school, date.Date, feeCycle);
            }
            if (Transactions == null || Transactions.Length == 0)
            {
                if (date.Date > feeCycle.LastDueDate.ToLocalTime().Date)
                {
                    var existingLateFee = LateFee;
                    if (existingLateFee != null && LateFeeDate != null || existingLateFee == 0)
                    {
                        return totalAmount + LateFee.Value;
                    }


                    return totalAmount + totalLatefee;
                }
                else
                {
                    return totalAmount;
                }
            }

            var totalPaid = Transactions.Sum(x => x.Amount);
            var totalRemaining = totalAmount - totalPaid;

            if (date.Date > feeCycle.LastDueDate.ToLocalTime().Date)
            {
                if (LateFee != null)
                {
                    totalRemaining = totalRemaining + LateFee.Value;
                }
                else
                {
                    totalRemaining = totalRemaining + totalLatefee;
                }
            }
            if (totalRemaining <= 0)
            {
                return 0;
            }
            return totalRemaining;
        }

        public double GetOldPendingFee(DateTime date, FeeCycle feeCycle, School school)
        {
            var totalAmount = GetTotalFees();
            if (totalAmount <= 0)
            {
                return 0;
            }
            double totalLatefee = 0;
            if (date.Date > feeCycle.LastDueDate.ToLocalTime().Date)
            {
                totalLatefee = GetTotalLateFees(school, date.Date, feeCycle);
            }

            if (Transactions == null || Transactions.Length == 0)
            {
                if (date.Date > feeCycle.LastDueDate.ToLocalTime().Date)
                {
                    var existingLateFee = LateFee;
                    if (existingLateFee != null && LateFeeDate != null || existingLateFee == 0)
                    {
                        return totalAmount + LateFee.Value;
                    }


                    return totalAmount + totalLatefee;
                }
                else
                {
                    return totalAmount;
                }
            }

            var totalPaid = Transactions.Sum(x => x.Amount);
            var totalRemaining = totalAmount - totalPaid;

            if (date.Date > feeCycle.LastDueDate.ToLocalTime().Date)
            {
                if (LateFee != null)
                {
                    totalRemaining = totalRemaining + LateFee.Value;
                }
                else
                {
                    totalRemaining = totalRemaining + totalLatefee;
                }
            }
            if (totalRemaining <= 0)
            {
                return 0;
            }
            return totalRemaining;
        }
    }

    public class Component
    {
        public ObjectId ComponetId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public class TransactionForFeeModel
    {
        public TransactionForFeeModel()
        {
            Components = new Collection<Component>();
        }
        public ObjectId TransactionId { get; set; }
        public double Amount { get; set; }

        public ICollection<Component> Components { get; set; }
    }
}
