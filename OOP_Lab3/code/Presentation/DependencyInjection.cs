using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_LAB3.Application.Interfaces;
using OOP_LAB3.Application.Services;
using OOP_LAB3.DataAccess.Repositories;
using OOP_LAB3.Presentation.Commands;
using OOP_LAB3.DataAccess.Api;

namespace OOP_LAB3.Presentation
{
    public static class DependencyInjection
    {
        public static List<ICommand> Register()
        {
            var testAdapter = new QuoteAdapter();
            //var quote = await testAdapter.GetRandomQuoteAsync();
            //Console.WriteLine($"API Test Result: {quote.Content} - {quote.Author}\n");

            var repository = new StudentRepository("students.json");
            var service = new StudentService(repository, testAdapter);

            var addCommand = new AddStudentCommand(service);
            var editCommand = new EditStudentCommand(service);
            var viewCommand = new ViewStudentsCommand(service);
            var ret = new List<ICommand>();
            ret.Add(addCommand);
            ret.Add(editCommand);
            ret.Add(viewCommand);
            return ret;
        }
    }
}
