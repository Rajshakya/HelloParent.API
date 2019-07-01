using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GAPS.Kidzo.API.Views
{
    public class FeeFilterView
    {
        public string[] FeeStatus { get; set; }
    
        public string ClassId { get; set; }
      
        public string FeeCycleId { get; set; }
        public bool IsNotification { get; set; }
        public string StudentId { get; set; }
        public string SessionId { get; set; }
        public string StudentFeeFrequency { get; set; }
        public string ActiveSessionName { get; set; }
        public int ToCheckUnapprovedStudents { get; set; }
    }
}
