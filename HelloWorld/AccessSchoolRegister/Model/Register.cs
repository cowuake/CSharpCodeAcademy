using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MainLibrary;

namespace AccessSchoolRegister.Model
{
    public class Register
    {
        private List<Student> ListOfStudents;

        public Register()
        {
            this.ListOfStudents = new List<Student>();
        }

        private string ReadFirstName()
        {
            return InputLib.ReadFromConsoleConditionally(
                "Insert student's first name: ", s => s.All(c => Char.IsLetter(c)));
        }

        private string ReadLastName()
        {
            return InputLib.ReadFromConsoleConditionally(
                "Insert student's last name: ", s => s.All(c => Char.IsLetter(c)));
        }

        private DateTime ReadDate()
        {
            return InputLib.ReadDateTimeFromConsole("Insert student's date of birth: ");
        }

        public void AddStudent()
        {
            Console.WriteLine("A new student will be added.");

            string firstName = this.ReadFirstName();
            string lastName = this.ReadLastName();
            DateTime birthDay = this.ReadDate();

            this.ListOfStudents.Add(new Student(firstName, lastName, birthDay));
        }

        public void DeleteStudent()
        {
            string firstName = this.ReadFirstName();
            string lastName = this.ReadLastName();

            int index;

            if (this.IndexOfStudent(firstName, lastName, out index))
            {
                this.ListOfStudents.RemoveAt(index);
            }
        }

        private bool IndexOfStudent(string firstName, string lastName, out int index)
        {
            index = -1;

            if (this.ListOfStudents.Count == 0)
            {
                return false;
            }

            index = this.ListOfStudents.FindIndex(
                student => student.FirstName == firstName && student.LastName == lastName);

            return true;
        }

        public void SearchStudent()
        {
            string firstName = this.ReadFirstName();
            string lastName = this.ReadLastName();

            int index;

            if (this.IndexOfStudent(firstName, lastName, out index))
            {
                Student student = this.ListOfStudents[index];
                Console.WriteLine();
                Console.WriteLine($"Student: {student.FirstName} {student.LastName}");
                Console.WriteLine($"History of {student.FirstName}'s results:");
                this.ListStudentResults(student);
            }
        }

        public void ListStudents()
        {
            if (this.ListOfStudents.Count == 0)
            {
                Console.WriteLine("No students in the register yet.");
                return;
            }

            for (int i = 0; i < this.ListOfStudents.Count; i++)
            {
                int nResults = this.ListOfStudents[i].Results.Count;
                float avgGrade = 0;

                string avgString = "";

                if (nResults != 0)
                {
                    foreach (TestEvaluation result in this.ListOfStudents[i].Results)
                    {
                        avgGrade += result.Grade;
                    }

                    avgGrade /= nResults;
                    avgString += $", average grade: {avgGrade}";
                }

                Console.WriteLine($"{i + 1}:\t{ListOfStudents[i].FirstName} {ListOfStudents[i].LastName}" + avgString);
            }
        }

        private Student ChooseStudent()
        {
            this.ListStudents();
            int index = InputLib.ReadIntegerFromConsole("Choose the student by number: ",
                i => i >= 1 && i <= this.ListOfStudents.Count) - 1;

            Console.WriteLine();

            return this.ListOfStudents[index];
        }

        private TestEvaluation ChooseTestResult(Student student) // ! Move to Student class
        {
            this.ListStudentResults(student);
            int index = InputLib.ReadIntegerFromConsole("Choose the test result by number: ",
                i => i >= 1 && i <= student.Results.Count) - 1;

            return student.Results[index];
        }

        private bool CheckStudents()
        {
            if (this.ListOfStudents.Count == 0)
            {
                Console.WriteLine("No student stored in register to this point.");
                return false;
            }

            return true;
        }
        public void InsertTestResults()
        {
            if (!CheckStudents())
            {
                return;
            }

            Student student = this.ChooseStudent();

            Console.Write("Insert a description for the test: ");
            string description = Console.ReadLine();

            byte grade = InputLib.ReadByteFromConsole("Insert achieved grade [1 - 10]: ", b => b > 0 && b <= 10);

            DateTime date = InputLib.ReadDateTimeFromConsole("Insert date of the test: ");

            var type = TestType.Laboratory;

            student.Results.Add(new TestEvaluation(description, grade, type));
        }

        private void ListStudentResults(Student student)
        {
            if (student.Results.Count == 0)
            {
                Console.WriteLine($"No test results available for student {student.FirstName} {student.LastName}.");
                return;
            }

            //Console.WriteLine($"The following test results were saved for student {student.FirstName} {student.LastName}");
            Console.WriteLine("");

            for (int i = 0; i < student.Results.Count; i++)
            {
                Console.WriteLine($"Test {i + 1}:\t{student.Results[i].Description}");
                Console.WriteLine($"Grade: {student.Results[i].Grade}");
                Console.WriteLine($"Date: {student.Results[i].Date}");
                Console.WriteLine();
            }
        }

        public void DeleteTestResults()
        {
            if (!CheckStudents())
            {
                return;
            }

            Student student = this.ChooseStudent();
            TestEvaluation result = this.ChooseTestResult(student);

            student.Results.Remove(result);
        }

        public void SaveRegisterState()
        {
            Console.WriteLine();
        }
    }
}
