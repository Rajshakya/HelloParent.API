
using HelloParent.Entities;
using HelloParent.Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloParent.Entities.Model
{
    [BsonIgnoreExtraElements]
    public class Student : BaseEntity
    {
        public Student()
        {
            FeeComponets = new StudentFeeComponent[] { };
            AuthorizedPersons = new AuthorizedPerson[] { };
            CabIds = new ObjectId[] { };
            HealthDetails = new HealthDetails();
        }

        public ObjectId SchoolId { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }

        public string FirstName
        {
            get
            {
                return String.IsNullOrEmpty(Name)
                    ? string.Empty
                    : Name.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
        }

        public DateTime DateOfBirth { get; set; }
        public Boolean Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string FatherContactNo { get; set; }
        public string MotherContactNo { get; set; }
        public string EmergencyContactNo { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        ///For Pickup ,droping and category addresses
        public string PickAddress { get; set; }
        public string DropAddress { get; set; }
        public string Category { get; set; }

        public string FatherImage { get; set; }
        public string MotherImage { get; set; }
        public bool CabRequired { get; set; }

        public ObjectId? CabId { get; set; }
        public ObjectId[] CabIds { get; set; }
        public string EmergencyName { get; set; }

        public string ParentCode { get; set; }
        public string BloodGroup { get; set; }

        public ObjectId ClassId { get; set; }
        public double SecurityAmount { get; set; }
        public DateTime? SecurityRefundDate { get; set; }
        public string RefundNote { get; set; }
        public DateTime? JoiningDate { get; set; }

        public StudentFeeComponent[] FeeComponets { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public string Notes { get; set; }
        public string FeeNotes { get; set; }
        public void SetParentCode()
        {
            ParentCode = Guid.NewGuid().ToString().Replace("-", "");
        }
        public StudentFeeFrequency FeeFrequency { get; set; }

        public ICollection<AuthorizedPerson> AuthorizedPersons { get; set; }
        public HealthDetails HealthDetails { get; set; }


    }

    public class StudentFeeComponent
    {
        public ObjectId Id { get; set; }

        public ObjectId ClassFeeComponentId { get; set; }
        public ObjectId SchoolFeeComponentId { get; set; }
        public double Value { get; set; }
    }

    public class AuthorizedPerson
    {

        public string Name { get; set; }
        public string Relation { get; set; }

        public string Id { get; set; }

        public string Image { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

    }

    public class HealthDetails
    {
        public HealthDetails()
        {
            ChildHad = new string[] { };
            SuffersFrom = new string[] { };
        }
        public string RegularMedication { get; set; }
        public string MedicineAllergies { get; set; }
        public string FoodAllergies { get; set; }
        public string OtherAllergies { get; set; }
        public string SpecialCondition { get; set; }
        public string Physician { get; set; }
        public string PhysicianContactNo { get; set; }
        public string PhysicianEmail { get; set; }
        public string Dentist { get; set; }
        public string DentistContactNo { get; set; }
        public string DentistEmail { get; set; }
        public string Hospital { get; set; }
        public string HospitalContactNo { get; set; }
        public string HospitalEmail { get; set; }
        public string[] ChildHad { get; set; }
        public string[] SuffersFrom { get; set; }
    }
}
