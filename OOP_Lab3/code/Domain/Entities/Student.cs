using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB3.Domain.Entities
{
    public class Student
    {
        public Student(string name, int grade)
        {
            Name = name;
            Grade = grade;
        }

        public string Name { get; private set; }
        public int Grade { get; private set; }

    }
}
