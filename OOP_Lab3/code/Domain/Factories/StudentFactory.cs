using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_LAB3.Domain.Entities;
using OOP_LAB3.Domain.Validators;
namespace OOP_LAB3.Domain.Factories
{
    public static class StudentFactory
    {
        public static Student Create(string Name,int Grade)
        {
            return new Student(Name, Grade);
        }
    }
}
