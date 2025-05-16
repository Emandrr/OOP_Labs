using OOP_LAB3.Presentation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOP_LAB3.Presentation.UI
{
    public class UI
    {
        private readonly AddStudentCommand _addCommand;
        private readonly EditStudentCommand _editCommand;
        private readonly ViewStudentsCommand _viewCommand;

        public UI(List<OOP_LAB3.Application.Interfaces.ICommand> lst)
        {
            _addCommand = (AddStudentCommand)lst[0];
            _editCommand = (EditStudentCommand)lst[1];
            _viewCommand = (ViewStudentsCommand)lst[2];
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.WriteLine("1. Add Student\n2. Edit Student\n3. View Students\n4. Exit\n");
                Console.Write("Choose option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        await _addCommand.ExecuteAsync();
                        break;
                    case "2":
                        await _editCommand.ExecuteAsync();
                        break;
                    case "3":
                        await _viewCommand.ExecuteAsync();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}
