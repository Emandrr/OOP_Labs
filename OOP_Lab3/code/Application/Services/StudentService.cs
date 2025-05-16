using OOP_LAB3.Application.DTO;
using OOP_LAB3.DataAccess.Repositories;
using OOP_LAB3.DataAccess.Api;
using OOP_LAB3.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB3.Application.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repository;
        private readonly QuoteAdapter _quoteAdapter;

        public StudentService(StudentRepository repository, QuoteAdapter quoteAdapter)
        {
            _repository = repository;
            _quoteAdapter = quoteAdapter;
        }

        public async Task<(StudentDTO student, QuoteDTO quote)> AddStudentAsync(StudentDTO studentDto)
        {
            var student = StudentFactory.Create(studentDto.Name, studentDto.Grade);
            _repository.Add(student);

            var quote = await _quoteAdapter.GetRandomQuoteAsync();
            return (studentDto, quote);
        }

        public List<StudentDTO> GetAllStudents()
        {
            return _repository.GetAll()
                .Select(s => new StudentDTO(s.Name, s.Grade))
                .ToList();
        }

        public void UpdateStudent(int index, StudentDTO studentDto)
        {
            var student = StudentFactory.Create(studentDto.Name, studentDto.Grade);
            _repository.Update(index, student);
        }
    }
}
