using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AccessSchoolRegister.Model
{
    public class Student
    {
        public string FirstName { get; }

        public string LastName { get; }

        public string FullName => $"{FirstName} {FullName}";

        public DateTime BirthDay { get; }

        public List<TestEvaluation> Results = new List<TestEvaluation>();

        public float Average
        {
            get
            {
                if (Results != null)
                {
                    return (float)Results.Average(x => x.Grade);
                }
                else
                {
                    return 0;
                }    
            }
        }

        public Student(string firstName, string lastName, DateTime birthDay)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDay = birthDay;
        }
    }
}
