using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB3.Domain.Validators
{
    public class StudentValidator
    {
        public static void Validate(string name,int grade)
        { 
            if(string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be empty");
            if (grade < 0 || grade > 100) throw new ArgumentException("Grade must be between 0 and 100");
        }
    }
}
