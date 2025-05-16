using OOP_LAB3.Application.DTO;
using OOP_LAB3.Application.Services;
using OOP_LAB3.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB3.Presentation.Commands
{
    public class AddStudentCommand : ICommand
    {
        private readonly StudentService _service;

        public AddStudentCommand(StudentService service)
        {
            _service = service;
        }

        public async Task ExecuteAsync()
        {
            Console.Write("Enter student name: ");
            var name = Console.ReadLine();

            Console.Write("Enter student grade: ");
            if (!int.TryParse(Console.ReadLine(), out int grade))
            {
                Console.WriteLine("Invalid grade format!");
                return;
            }

            try
            {
                var result = await _service.AddStudentAsync(new StudentDTO(name, grade));
                Console.WriteLine($"Student added successfully!");
                Console.WriteLine($"Motivational Quote: {result.quote.Content} - {result.quote.Author}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
