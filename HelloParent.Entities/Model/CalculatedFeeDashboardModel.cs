using HelloParent.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelloParent.Entities.Model
{
    public class CalculatedFeeDashboardModel
    {
        public CalculatedFeeDashboardModel()
        {
            ClassDict = new Dictionary<string, string>();

            FeeCycleDict = new Dictionary<string, string>();
            FeeStudents = new FeeStudentModel[] { };
            FeeComponentWithDictinctValueDict = new Dictionary<string, double>();
            StudentDict = new Dictionary<string, string>();
            StudentFeeFrequencyDictionary = new Dictionary<string, string>();
            FeeStatusDictionary = new Dictionary<string, string>();

        }
        [Required(ErrorMessage = "Please choose a class")]
        public string ClassId { get; set; }
        [Required(ErrorMessage = "Please choose a fee cycle")]
        public string FeeCycleId { get; set; }

        public bool IsNotification { get; set; }
        public IDictionary<string, string> ClassDict { get; set; }

        public IDictionary<string, string> FeeCycleDict { get; set; }
        public FeeStudentModel[] FeeStudents { get; set; }
        public FeeStudentModel Maxdata { get; set; }
        public IDictionary<string, double> FeeComponentWithDictinctValueDict { get; set; }
        public int ToCheckUnapprovedStudents { get; set; }

        public string Command { get; set; }

        public string Reason { get; set; }
        public string ReasonToSend { get; set; }
        public string StudentId { get; set; }
        public IDictionary<string, string> StudentDict { get; set; }
        public string ActiveSessionName { get; set; }
        public string StudentFeeFrequency { get; set; }
        public string[] FeeStatus { get; set; }
        public IDictionary<string, string> StudentFeeFrequencyDictionary { get; set; }
        public IDictionary<string, string> FeeStatusDictionary { get; set; }
        public Dictionary<string, string> SessionDict { get; set; }
        public string SessionId { get; set; }
    }
    
    public class FeeStudentModelView
    {
        public FeeStudentModel Maxdata { get; set; }
        public FeeStudentModel[] FeeStudents { get; set; }
        public IDictionary<string, double> FeeComponentWithDictinctValueDict { get; set; }
    }
    public class FeeStudentModel
    {
        public FeeStudentModel()
        {
            StudentAllFee = new FeeModel[] { };
            Transactions = new TransactionForFeeModel[] { };
            ComponetDict = new Dictionary<string, double>();
        }
        public string StudentId { get; set; }
        public string AdmNumber { get; set; }
        public string StudentName { get; set; }
        public string StudentMotherName { get; set; }
        public string ClassName { get; set; }

        public string StudentClass { get; set; }
        public double TotalFeeForStudent { get; set; }
        public FeeModel[] StudentAllFee { get; set; }
        public TransactionForFeeModel[] Transactions { get; set; }
        public string FeeId { get; set; }
        public string ApprovedById { get; set; }
        public string CancelledById { get; set; }
        public double TotalPayable { get; set; }
        public double TotalPaid { get; set; }
        public string FeeStatus { get; set; }
        public FeeStatus FeeStatusEnum { get; set; }

        public FeeCycleSingleModel FeeCycle { get; set; }
        public string Remark { get; set; }
        public DateTime CreateAt { get; set; }
        public double? LateFee { get; set; }
        public IDictionary<string, double> ComponetDict { get; set; }
        public string StudentFeeFrequency { get; set; }
        public string FeeCycleName { get; set; }
        public bool Checked {get;set;}
       

    }
    public class FeeApproveModel

    {
        public FeeApproveModel()
        {
            FeeIds = new string[] { };
        }
        public string[] FeeIds { get; set; }

        public string ClassId { get; set; }

        public string FeeCycleId { get; set; }
        public string Command { get; set; }
        public string ReasonToSend { get; set; }
        public string StudentId { get; set; }
        public bool IsNotification { get; set; }

    }


    public class FeeModel
    {
        public string ComponentName { get; set; }
        public double ComponentValue { get; set; }
        public string SchoolFeeComponentId { get; set; }

    }

    public class CalculateFeeModel
    {
        public CalculateFeeModel()
        {
            ClassDict = new Dictionary<string, string>();
            SessionDict = new Dictionary<string, string>();
        }
        public IEnumerable<FeeCycleSingleModel> FeeCycles { get; set; }
        public IDictionary<string, string> SessionDict { get; set; }
        public string SessionId { get; set; }
        public FeeCycleSingleModel FeeCycle { get; set; }
        public string FeeCycleId { get; set; }
        public string SchoolId { get; set; }
        public string ClassId { get; set; }
        public IDictionary<string, string> ClassDict { get; set; }
    }
}
