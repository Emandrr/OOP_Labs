using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OOP_Lab2
{
    public class MainMenu
    {
        string input = "";
        int choise = 0;
        string output;
        Checker checker = new Checker();
        List<User> Users;
        ManageUserFile manager = new ManageUserFile();
        WorkWithUsers Worker = new WorkWithUsers();
        public MainMenu()
        {
            Users = manager.GetUsers();
            if (Users == null) Users = new List<User>();
        }
        public void Start()
        {
            while (true) {
                output = "0 - Создать пользователя, 1 - Выбрать пользователя для входа, -1 - Выход";
                Console.WriteLine(output);
                input = Console.ReadLine();
                choise = checker.CheckWithBorders(input, -1, 1, output, 2);
                if (choise == 0)
                {
                    output = "Введите имя пользователя";
                    Console.WriteLine(output);
                    string name = Console.ReadLine();
                    while (checker.CheckStringInCollection(Users, name))
                    {
                        Clear(2);
                        Console.WriteLine("Ошибка такой пользователь есть! " + output);
                        name = Console.ReadLine();
                    }
                    Worker.Add(Users,name);
                    Clear(4);
                }
                else if( choise == 1)
                {
                    Console.WriteLine("Список всех пользователей");
                    Worker.Print(Users);
                    output = "Введите имя пользователя";
                    Console.WriteLine(output);
                    string UserName = Console.ReadLine();
                    while(!checker.CheckStringInCollection(Users,UserName))
                    {
                        Clear(2);
                        Console.WriteLine("Ошибка такого пользователя нет "+output);
                        UserName = Console.ReadLine();
                    }
                    Clear(6);
                    Console.WriteLine(UserName);

                    foreach(User a in Users)
                    {
                        if (a.Name == UserName) a.ExecuteStrategy();
                    }
                }
                else if (choise == -1)
                {
                    manager.WriteUsers(Users);
                    return;
                }
            }
        }
        public void Clear(int count)
        {
            int tp = Console.GetCursorPosition().Top;
            for (int i = tp; i < tp + count ; ++i)
            {
                Console.SetCursorPosition(0, i - count);
                Console.Write("\r");
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, tp-count);
        }
        
    }
}