using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using OOP_LAB3.Domain.Entities;
namespace OOP_LAB3.DataAccess.Repositories
{
    public class StudentRepository
    {
        private readonly string filePath;
        private List<Student> ListOfStudents;

        public StudentRepository(string filePath)
        {
            this.filePath = filePath;
            Load();
        }
        public void Add(Student student)
        {
            ListOfStudents.Add(student);
            Save();
        }
        public void Save()
        {
            var json = JsonSerializer.Serialize(ListOfStudents);
            File.WriteAllText(filePath,json);
        }
        public void Load()
        {
            if(File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                ListOfStudents = JsonSerializer.Deserialize<List<Student>>(json);
            }
            else
            {
                ListOfStudents = new List<Student>();
            }
        }
        public List<Student> GetAll() => new List<Student>(ListOfStudents);
        public void Update(int index,Student student)
        {
            if (index < 0 || index >= ListOfStudents.Count) throw new ArgumentOutOfRangeException(nameof(index));

            ListOfStudents[index] = student;
            Save();
        }
    }
}
