using System;
using System.Collections.Generic;
using System.Text;

namespace AccessSchoolRegister.Model
{
    public class Student
    {
        public string FirstName { get; }

        public string LastName { get; }

        public DateTime BirthDay { get; }

        public List<TestEvaluation> Results = new List<TestEvaluation>();

        public Student(string firstName, string lastName, DateTime birthDay)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDay = birthDay;
        }
    }
}
