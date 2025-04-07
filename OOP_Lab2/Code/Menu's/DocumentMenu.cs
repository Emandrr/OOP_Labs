using Google.Apis.Drive.v3.Data;
using OOP_Lab2.UserStrategy;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OOP_Lab2.Menu_s
{
    class DocumentMenu
    {
        string RoleToShow;
        WorkWithUsers Worker = new WorkWithUsers();
        List<User> Users;
        Checker checker = new Checker();
        IUserStrategy strat;
        ManageMemFile<User> Manager = new ManageMemFile<User>();
        Document Document;
        Settings set;
        public DocumentMenu(string role,IUserStrategy admin, Document document,Settings set)
        {
            this.RoleToShow = role;
            this.strat = admin;
            Document = document;
            this.set = set;

        }
        public void AdminMenu()
        {
            while (true)
            {
                set.SetConsoleFont(14);
                Users = Manager.GetCollection("mem.json");
                Console.WriteLine(Document.name);
                string output = "0 - Поменять роль у пользователя, 1 - Редактировать файл, 2 - Просмотреть файл, 3 - Удалить файл, -1 - Выход";
                Console.WriteLine(output);
                int choise = checker.CheckWithBorders(Console.ReadLine(), -1, 4, output, 2);
                if (choise == 0)
                {
                    Console.WriteLine("Список всех пользователей");
                    Worker.Print(Users);
                    output = "Введите имя пользователя";
                    Console.WriteLine(output);
                    string UserName = Console.ReadLine();
                    while (!checker.CheckStringInCollection(Users, UserName))
                    {
                        Clear(2);
                        Console.WriteLine("Ошибка такого пользователя нет " + output);
                        UserName = Console.ReadLine();
                    }
                    output = "Выберете роль: 0 - reader, 1 - writer ";
                    Console.WriteLine(output);
                    choise = checker.CheckWithBorders(Console.ReadLine(), 0, 1, output, 2);
                    if (choise == 0)
                    {
                        if (strat is AdminStrategy admin)
                        {
                            admin.ChangeRole(Document, Users, "reader", UserName);
                        }
                    }
                    if (choise == 1)
                    {
                        if (strat is AdminStrategy admin)
                        {
                            admin.ChangeRole(Document, Users, "writer", UserName);
                        }
                    }
                    Manager.WriteCollection(Users, "mem.json");
                    //Clear(8);
                }
                else if (choise == 1 )
                {
                    EditorMenu();
                }
                else if(choise == 2)
                {
                    ReaderMenu();
                }
                else if (choise == 3)
                {
                    //ChangeStyleMenu();
                }
                else if (choise == -1)
                {
                    Console.Clear();
                    return;
                }
                Console.Clear();
                //set.SetConsoleFont(14);
                //Console.WriteLine(Document.name);
            }
        }
        public void Clear(int count)
        {
            int tp = Console.GetCursorPosition().Top;
            for (int i = tp; i < tp + count; ++i)
            {
                if (i - count < 0) break;
                Console.SetCursorPosition(0, i - count);
                Console.Write("\r");
                Console.Write(new string(' ', Console.WindowWidth));
            }
            if (tp - count > 0) Console.SetCursorPosition(0, tp - count);
            else Console.SetCursorPosition(0, 0);
        }
        public void ClearAll()
        {
            int tp = Console.GetCursorPosition().Top;
            for (int i = tp; i >-1; --i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("\r");
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 0);
        }
        public void EditorMenu()
        {
            bool flag1 = true;
            bool flag = true;
            int pos = 0;
            int pos_r = 0;
            int pos_l = 0;
            string txt="";
            string buff="";
            while (true)
            {
                if (flag) Console.Clear();
                if (flag) if (strat is AdminStrategy s)
                    {
                        txt = s.Read();
                        Console.WriteLine(txt);
                    }
                string output = "esc - выход,ctrl+1 - вниз,ctrl+2 вверх,ctrl+3 - влево,ctrl+4 вправо,ctrl+f - поиск слова,shift+> выделить ";
                
                if(flag)Console.WriteLine(output);
                if (flag) pos = Console.GetCursorPosition().Top;
               if(flag&&pos-2>0) Console.SetCursorPosition(0, pos-2);
               else if(flag)
                {
                    Console.SetCursorPosition(0, pos - 1);

                }


                    var k = Console.ReadKey();
                if (k.Key == System.ConsoleKey.D2 && k.Modifiers == ConsoleModifiers.Control)
                {
                    pos_l = pos_r = 0;
                    flag = false;
                    //Clear(1);
                    if (Console.GetCursorPosition().Top > 0) Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top - 1);

                    //Console.Clear();
                }
                //up
                if (k.Key == System.ConsoleKey.D1 && k.Modifiers == ConsoleModifiers.Control)
                {
                    pos_l = pos_r = 0;
                    flag = false;
                    if (Console.GetCursorPosition().Top < pos-2) Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top + 1);
                    //Clear(1);
                    //Console.Clear();
                }
                if (k.Key == System.ConsoleKey.D3 && k.Modifiers == ConsoleModifiers.Control)
                {
                    //Clear(1);
                    if (Console.GetCursorPosition().Left > 0) Console.SetCursorPosition(Console.GetCursorPosition().Left - 1, Console.GetCursorPosition().Top);
                    flag = false;
                    //Console.Clear();
                }
                //up
                if (k.Key == System.ConsoleKey.D4 && k.Modifiers == ConsoleModifiers.Control)
                {
                    pos_l = pos_r = 0;
                    if (Console.GetCursorPosition().Left < 170) Console.SetCursorPosition(Console.GetCursorPosition().Left + 1, Console.GetCursorPosition().Top);
                    //Clear(1);
                    flag = false;
                    //Console.Clear();
                }
                if(k.Key == System.ConsoleKey.F && k.Modifiers == ConsoleModifiers.Control)
                {
                    pos_l = pos_r = 0;
                    Console.SetCursorPosition(0, pos);
                    output = "-1 - выход, введенная строка и enter - показ";
                    Console.WriteLine(output);
                    while (true)
                    {
                        
                        output = "-1 - выход, введенная строка и enter - показ";
                        
                        string st1 = Console.ReadLine();
                        if (st1 == "-1")
                        {
                            Clear(2);
                            break;
                        }
                        else if(st1.Length==0)
                        {
                            Clear(1);
                            continue;
                        }
                        else
                        {
                            SearchString(txt,st1);
                            Console.WriteLine(output);
                        }
                    }
                    
                }
                if (k.Key == System.ConsoleKey.RightArrow && k.Modifiers == ConsoleModifiers.Shift)
                {
                    if (Console.GetCursorPosition().Left < 170) Console.SetCursorPosition(Console.GetCursorPosition().Left + 1, Console.GetCursorPosition().Top);
                    SetRight(ref txt,output,pos,ref pos_r,ref pos_l,ref buff);
                    flag1 = false;
                    flag = false;
                }
                if (k.Key == System.ConsoleKey.LeftArrow && k.Modifiers == ConsoleModifiers.Shift)
                {
                    if (Console.GetCursorPosition().Left >0) Console.SetCursorPosition(Console.GetCursorPosition().Left -1 , Console.GetCursorPosition().Top);
                    SetLeft(ref txt, output, pos, ref pos_r, ref pos_l, ref buff);
                    flag1 = false;
                    flag = false;
                }
                if((k.KeyChar>=97 && k.KeyChar <=122 )|| (k.KeyChar >= 65 && k.KeyChar <= 90))
                {
                    if (strat is AdminStrategy s) s.ModifyUp(ref txt,Console.GetCursorPosition().Top, Console.GetCursorPosition().Left, k.KeyChar);
                }
                if (k.Key == System.ConsoleKey.Escape)
                {
                    flag = false;
                    break;
                }


                // return;
            }
        }
        public void SetRight(ref string txt,string ouput,int pos,ref int p,ref int l, ref string buff)
        {
            string[] tmp = txt.Split("\n");
            int pos1 = Console.GetCursorPosition().Top;
            int pos2 = Console.GetCursorPosition().Left;
            Console.SetCursorPosition(0, pos+1);
            if (buff.Length >= 1) Clear(1);
            if (l > 0)
            {
                l--;
                buff =buff.Remove(0);
            }
            else
            {
                if (pos1 - 1 > -1 && pos2 - 1 <= tmp[pos1 - 1].Length - 1) buff += tmp[pos1 - 1][pos2 - 1];
                p++;
            }
            
            Console.SetCursorPosition(0, pos);
           // Clear(1);
            
            Console.Write(buff);
            Console.SetCursorPosition(pos2,pos1);
        }
        public void SetLeft(ref string txt, string ouput, int pos, ref int p, ref int l, ref string buff)
        {
            string[] tmp = txt.Split("\n");
            int pos1 = Console.GetCursorPosition().Top;
            int pos2 = Console.GetCursorPosition().Left;
            Console.SetCursorPosition(0, pos+1);
            if (buff.Length >= 1) Clear(1);
            //Console.Write(tmp[pos1 - 1][pos2 - 1]);
            if (p > 0)
            {
                p--;
               buff =  buff.Remove(buff.Length-1);
            }
            else
            {
              if(pos1-1>-1 && pos2 - 1 <= tmp[pos1-1].Length-1)  buff += tmp[pos1 - 1][pos2 - 1];
                l++;
            }
            //Clear(1);
            Console.SetCursorPosition(0, pos);
           
            Console.Write(buff);
            Console.SetCursorPosition(pos2, pos1);
        }
        public void ReaderMenu()
        {
            short size = 14;
            while (true)
            {
                Console.Clear();   
                int pos = Console.GetCursorPosition().Top - 1;
                
                if (strat is AdminStrategy s) Console.WriteLine(s.Read());
                string output = "ctrl+k сохранить файл, ctrl+o - размер шрифта,ctrl+l - сменить тему,  -1 выход";
                Console.WriteLine(output);
                string readkey = Console.ReadLine();
                if(readkey=="\f")
                {
                    output = "0 - Campbell, 1 - One Half Dark, 2 - Solarized Dark, 3 - Tango Dark, 4 - Vintage";
                    Console.WriteLine(output);
                    short chose = (short)checker.CheckWithBorders(Console.ReadLine(), 0, 4, output, 2);
                    if (chose == 0)
                    {
                        set.SetConsoleTheme("Campbell");
                    }
                    if (chose == 1)
                    {
                        set.SetConsoleTheme("One Half Dark");
                    }
                    if (chose == 2)
                    {
                        set.SetConsoleTheme("Solarized Dark");
                    }
                    if (chose == 3)
                    {
                        set.SetConsoleTheme("Tango Dark");
                    }
                    if (chose == 4)
                    {
                        set.SetConsoleTheme("Vintage");
                    }
                    Clear(4);
                }
                if(readkey=="\u000f")
                {
                    while (true)
                    {
                        output = "esc - выход,ctrl+1 - увеличить размер шрифта,ctrl+2 уменьшить размер шрифта";
                        Console.WriteLine(output);
                        var k = Console.ReadKey();

                        if (k.Key == System.ConsoleKey.D2 && k.Modifiers == ConsoleModifiers.Control)
                        {

                            if (size > 11)
                            {
                                size--;
                                set.SetConsoleFont(size);

                            }

                        }
                        if (k.Key == System.ConsoleKey.D1 && k.Modifiers == ConsoleModifiers.Control)
                        {


                            if (size < 15)
                            {
                                size++;
                                set.SetConsoleFont(size);
                            }
                        }
                        if(k.Key ==System.ConsoleKey.Escape)
                        {
                            Console.Clear();
                            break;

                        }
                        Clear(1);
                    }
                }
                if(readkey=="\v")
                {

                }
                if (readkey == "-1")
                {
                    Console.Clear();
                    //ClearAll();
                    
                    return;
                    
                }

            }
        }
        public void SearchString(string txt,string st1)
        {
            const string grayColor = "\x1b[38;5;214m";
            const string resetColor = "\x1b[0m";

            var outpu = new StringBuilder();
            int index = 0;

            while (index < txt.Length)
            {
                int foundIndex = txt.IndexOf(st1, index, StringComparison.OrdinalIgnoreCase);

                if (foundIndex == -1)
                {
                    outpu.Append(txt.Substring(index));
                    break;
                }

                outpu.Append(txt.Substring(index, foundIndex - index));
                outpu.Append(grayColor);
                outpu.Append(txt.Substring(foundIndex, st1.Length));
                outpu.Append(resetColor);

                index = foundIndex + st1.Length;
            }
            Console.Clear();
            Console.WriteLine(outpu.ToString());

        }
           }
}
