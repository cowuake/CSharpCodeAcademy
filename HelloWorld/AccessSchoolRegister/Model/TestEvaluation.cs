using System;
using System.Collections.Generic;
using System.Text;

namespace AccessSchoolRegister.Model
{
    public class TestEvaluation
    {
        public DateTime Date { get; }

        public string Description { get; }

        public byte Grade { get; }

        public TestType TestType { get; }

        public TestEvaluation(string description, byte grade, TestType type, DateTime date)
        {
            this.Description = description;
            this.Grade = grade;
            this.TestType = type;
            this.Date = date;
        }

        public TestEvaluation(string description, byte grade, TestType type)
            : this(description, grade, type, DateTime.Now) { }
    }
}
