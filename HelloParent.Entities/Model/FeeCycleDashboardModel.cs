using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelloParent.Entities.Model
{
    class FeeCycleDashboardModel
    {
    }
    public class FeeCycleSingleModel
    {
        [Required(ErrorMessage = "Please enter fee cycle name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please choose start date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please choose end date")]
        public DateTime EndDate { get; set; }

        public string IdString { get; set; }
        public ObjectId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = "Please choose due date")]
        public DateTime LastDueDate { get; set; }
        public string SchoolId { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Please enter positive value not more than 2 decimals.")]

        public double LateFee { get; set; }
        public ObjectId SessionId { get; set; }
    }
}
