using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelloParent.Entities.Model
{
    public class FeeComponentViewModel
    {
        public string Id { get; set; }
        public string ClassFeeComponentId { get; set; }
        public string SchoolFeeComponentId { get; set; }
        [Required(ErrorMessage = "Component is required field")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Please enter a value upto 2 decimals")]
        public double Value { get; set; }
        [Required(ErrorMessage = "Name is required field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Periodicity is required field")]
        public string Periodicity { get; set; }
        public string ClassId { get; set; }
        public bool IsDelete { get; set; }
        public string StudentId { get; set; }
    }

    public class FeeCollectionComponentViewModel

    {
        public FeeCollectionComponentViewModel()
        {
            Components = new List<ComponentsViewModel>();

        }
        public IList<ComponentsViewModel> Components { get; set; }
        public string FeeId { get; set; }
        public string FeeCycleId { get; set; }
        public string ClassId { get; set; }
        public string Remark { get; set; }
        public string StudentId { get; set; }
        public string SessionId { get; set; }
        public string StudentFeeFrequency { get; set; }
        public string FeeStatus { get; set; }
        public bool OnlinePayment { get; set; }

    }

    public class ComponentsViewModel
    {
        [Required(ErrorMessage = "Please enter Component id")]

        public string ComponetId { get; set; }
        [Required(ErrorMessage = "Please enter component value")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Please enter a positive value upto 2 decimals")]

        public double Value { get; set; }
        public double Paid { get; set; }
        public string Name { get; set; }
    }
    
}
