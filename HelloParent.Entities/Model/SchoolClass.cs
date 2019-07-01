using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.Model
{
    public class SchoolClass : BaseEntity
    {
        public SchoolClass()
        {
            ClassTeacherIds = new List<string>();
            FeeComponets = new FeeComponent[] { };
            Subjects = new Subject[] { };
            GradedDisciplines = new GradedDiscipline[] { };
        }
        public string Name { get; set; }
        public string Session { get; set; }
        public ObjectId SessionId { get; set; }

        public ICollection<string> ClassTeacherIds { get; set; }
        public ObjectId SchoolId { get; set; }
        public FeeComponent[] FeeComponets { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public Subject[] Subjects { get; set; }
        public GradedDiscipline[] GradedDisciplines { get; set; }
    }
    public class FeeComponent
    {
        public ObjectId Id { get; set; }
        public ObjectId SchoolFeeComponentId { get; set; }
        public double Value { get; set; }

        public FeeComponent CreateNewCopy()
        {
            return new FeeComponent
            {
                Id = ObjectId.GenerateNewId(),
                SchoolFeeComponentId = SchoolFeeComponentId,
                Value = Value
            };
        }
    }
    public class Subject
    {
        public Subject()
        {
            TeacherIds = new string[] { };
        }
        public ObjectId Id { get; set; }
        public ObjectId MasterSubjectId { get; set; }

        public string[] TeacherIds { get; set; }
        public string Description { get; set; }

        public Subject CreateNewCopy()
        {
            return new Subject
            {
                Id = ObjectId.GenerateNewId(),
                MasterSubjectId = this.MasterSubjectId,
                TeacherIds = TeacherIds,
                Description = Description
            };
        }
    }
    public class GradedDiscipline
    {
        public GradedDiscipline()
        {
            TeacherIds = new string[] { };
        }
        public ObjectId Id { get; set; }
        public ObjectId MasterGradedDisciplineId { get; set; }

        public string[] TeacherIds { get; set; }
        public string Description { get; set; }

        public GradedDiscipline CreateNewCopy()
        {
            return new GradedDiscipline
            {
                Id = ObjectId.GenerateNewId(),
                MasterGradedDisciplineId = this.MasterGradedDisciplineId,
                TeacherIds = TeacherIds,
                Description = Description
            };
        }
    }
}
