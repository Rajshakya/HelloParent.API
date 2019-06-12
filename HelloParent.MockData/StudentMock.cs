
using HelloParent.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.MockData
{
    public class StudentMock
    {
        public List<Student> GetMockData()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student()
            {
                StudentId = ObjectId.Parse("5cc193643c099e17406b4ea9"),
                SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
                Name = "Karan Malthotra",
                FatherName = "Rohan Maltotra",
                MotherName = "Reena Maltotra",
                Identifier = "PS/01"

            });

            students.Add(new Student()
            {
                StudentId = ObjectId.Parse("5cc193643c099e17406b4eb9"),
                SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
                Name = "Ramandeep Singh",
                FatherName = "Gagandeep Singh",
                MotherName = "Gursharan Kaur",
                Identifier = "PS/02"

            });

            students.Add(new Student()
            {
                StudentId = ObjectId.Parse("5cc193643c099e17406b4ec9"),
                SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
                Name = "Rohan Garg",
                FatherName = "Bipin Garg",
                MotherName = "Neha Garg",
                Identifier = "PS/03"

            });

            students.Add(new Student()
            {
                StudentId = ObjectId.Parse("5cc193643c099e17406b4ed9"),
                SchoolId = ObjectId.Parse("56e45c3af289df1048faced3"),
                Name = "Diksha Soni",
                FatherName = "Nitin Soni",
                MotherName = "Pooja Soni",
                Identifier = "PS/04"

            });



            return students;
        }
    }
}

