using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP1;
class Drawer
{
   
    int x_c = 120;
    int y_c = 32;
    List<IFigure> list=new List<IFigure>();
    public void DrawCircle()
    {
        Console.WriteLine("Введите радиус круга");
        int radius = Int32.Parse(Console.ReadLine());
        Circle temp = new Circle();
            temp.CenterX = 120;
            temp.CenterY = 32;
            temp.radius = radius;
            temp.Color = 0;
            list.Add(temp);
        double thickness = 0.2;
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                double distance = Math.Sqrt(x * x + y * y); // Вычисляем расстояние от центра
                double difference = Math.Abs(distance - radius); // Отклонение от радиуса

                if (difference < thickness) // Если точка близка к границе
                {
                    Console.SetCursorPosition((x_c + x * 2), y_c + y);
                    Console.Write(".");
                }
                else if (distance < radius - thickness) // Если точка ВНУТРИ круга — меняем символ
                {
                    Console.SetCursorPosition((x_c + x * 2), y_c + y);
                    Console.Write(" "); // Символ внутри круга (можно заменить на любой)
                    Console.SetCursorPosition((x_c + x*2+1), y_c + y);
                    Console.Write(" "); // Символ внутри круга (можно заменить на любой)
                    Console.SetCursorPosition((x_c + x * 2 - 1), y_c + y);
                    Console.Write(" "); // Символ внутри круга (можно заменить на любой)

                }
            }
        }
    }
    public void DrawTriangle()
    {

        Console.WriteLine("Введите сторону треугольника");
        int a = Int32.Parse(Console.ReadLine());
        double thickness = 0.1; // Толщина границы
        Triangle temp = new Triangle();
        temp.CenterX = 120;
        temp.CenterY = 32;
        temp.radius = a;
        temp.Color = 0;
        list.Add(temp);
        int height = a;
        // Вычисляем координаты вершин правильного треугольника
        for (int y = 0; y < height; y++) // Перебираем строки треугольника
        {
            int startX = temp.CenterX - y; // Левая граница строки
            int endX = temp.CenterX + y;   // Правая граница строки
            int currentY = temp.CenterY - height + y; // Смещение по Y, чтобы центр был правильным

            // Верхняя точка (вершина)
            if (y == 0)
            {
                Console.SetCursorPosition(temp.CenterX, currentY);
                Console.Write(".");
            }
            // Основание (нижняя граница)
            else if (y == height - 1)
            {
                for (int x = startX; x <= endX; x++)
                {
                    Console.SetCursorPosition(x, currentY);
                    Console.Write(".");
                }
            }
            // Боковые границы (левая и правая)
            else
            {
                Console.SetCursorPosition(startX, currentY);
                Console.Write(".");

                Console.SetCursorPosition(endX, currentY);
                Console.Write(".");
            }

        }
        for (int y = 1; y < a - 1; y++) // Теперь включаем и последнюю строку
        {
            int startX = temp.CenterX - y + 1; // Начало заливки (на 1 правее левой границы)
            int endX = temp.CenterX + y - 1;   // Конец заливки (на 1 левее правой границы)
            int currentY = temp.CenterY - height + y; // Смещение по Y
            int smech = 0;

            for (int x = startX; x <= endX; x++) // Заполняем каждый второй символ
            {
                Console.SetCursorPosition(x, currentY);
                Console.Write(" "); // Символ заполнения
            }
        }
    }
    
    public void undraw(int param,bool inner_check)
    {
        IFigure tmp = list[param];
        if (tmp is Circle t)
        {
            double thickness = 0.2;
            for (int y = -t.radius; y <= t.radius; y++)
            {
                for (int x = -t.radius; x <= t.radius; x++)
                {
                    double distance = Math.Sqrt(x * x + y * y); // Вычисляем расстояние от центра
                    double difference = Math.Abs(distance - t.radius); // Отклонение от радиуса

                    if (difference < thickness) // Если точка близка к границе
                    {
                        Console.SetCursorPosition((t.CenterX + x * 2), t.CenterY + y);
                        Console.Write(" ");
                    }
                    else if (distance < t.radius - thickness) // Если точка ВНУТРИ круга — меняем символ
                    {
                        Console.SetCursorPosition((t.CenterX + x * 2), t.CenterY + y);
                        Console.Write(" "); // Символ внутри круга (можно заменить на любой)
                        Console.SetCursorPosition((t.CenterX + x * 2 + 1), t.CenterY + y);
                        Console.Write(" "); // Символ внутри круга (можно заменить на любой)
                        Console.SetCursorPosition((t.CenterX + x * 2 - 1), t.CenterY + y);
                        Console.Write(" "); // Символ внутри круга (можно заменить на любой)
                    }

                }
            }
            
        }
        if (tmp is Triangle tr)
        {
            for (int y = 0; y < tr.radius; y++) // Перебираем строки треугольника
            {
                int startX = tmp.CenterX - y; // Левая граница строки
                int endX = tmp.CenterX + y;   // Правая граница строки
                int currentY = tmp.CenterY - tr.radius + y; // Смещение по Y, чтобы центр был правильным

                // Верхняя точка (вершина)
                if (y == 0)
                {
                    Console.SetCursorPosition(tmp.CenterX, currentY);
                    Console.Write(" ");
                }
                // Основание (нижняя граница)
                else if (y == tr.radius - 1)
                {
                    for (int x = startX; x <= endX; x++)
                    {
                        Console.SetCursorPosition(x, currentY);
                        Console.Write(" ");
                    }
                }
                // Боковые границы (левая и правая)
                else
                {
                    Console.SetCursorPosition(startX, currentY);
                    Console.Write(" ");

                    Console.SetCursorPosition(endX, currentY);
                    Console.Write(" ");
                }

            }
            for (int y = 1; y < tr.radius - 1; y++) // Теперь включаем и последнюю строку
            {
                int startX = tmp.CenterX - y + 1; // Начало заливки (на 1 правее левой границы)
                int endX = tmp.CenterX + y - 1;   // Конец заливки (на 1 левее правой границы)
                int currentY = tmp.CenterY - tr.radius + y; // Смещение по Y

                for (int x = startX; x <= endX; x ++) // Заполняем каждый второй символ
                {
                    Console.SetCursorPosition(x, currentY);
                    Console.Write(" ");
                   
                }
            }

        }
        if (!inner_check) list.RemoveAt(param);
        if (!inner_check)
        {
            for (int i = 0; i <list.Count(); ++i)
            {
                Redraw(list[i]);
            }
        }
    }
    private void Redraw(IFigure temp)
    {
        if (temp is Circle t)
        {
            double thickness = 0.2;
            for (int y = -t.radius; y <= t.radius; y++)
            {
                for (int x = -t.radius; x <= t.radius; x++)
                {
                    double distance = Math.Sqrt(x * x + y * y); // Вычисляем расстояние от центра
                    double difference = Math.Abs(distance - t.radius); // Отклонение от радиуса

                    if (difference < thickness) // Если точка близка к границе
                    {
                        Console.SetCursorPosition((t.CenterX + x * 2), t.CenterY + y);
                        Console.Write(".");
                    }
                    else if (distance < t.radius - thickness) // Если точка ВНУТРИ круга — меняем символ
                    {
                        Console.SetCursorPosition((t.CenterX + x*2), t.CenterY + y);
                        if (t.Color == 0) Console.Write(" ");
                        if (t.Color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (t.Color == 2) Console.Write("#");
                        Console.SetCursorPosition((t.CenterX + x * 2+1), t.CenterY + y);
                        if (t.Color == 0) Console.Write(" ");
                        if (t.Color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (t.Color == 2) Console.Write("#");
                        Console.SetCursorPosition((t.CenterX + x * 2-1), t.CenterY + y);
                        if (t.Color == 0) Console.Write(" ");
                        if (t.Color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (t.Color == 2) Console.Write("#");

                    }

                }
            }
        }
       if(temp is Triangle tr)
        {
            for (int y = 0; y < tr.radius; y++) // Перебираем строки треугольника
            {
                int startX = temp.CenterX - y; // Левая граница строки
                int endX = temp.CenterX + y;   // Правая граница строки
                int currentY = temp.CenterY - tr.radius + y; // Смещение по Y, чтобы центр был правильным

                // Верхняя точка (вершина)
                if (y == 0)
                {
                    Console.SetCursorPosition(temp.CenterX, currentY);
                    Console.Write(".");
                }
                // Основание (нижняя граница)
                else if (y == tr.radius - 1)
                {
                    for (int x = startX; x <= endX; x++)
                    {
                        Console.SetCursorPosition(x, currentY);
                        Console.Write(".");
                    }
                }
                // Боковые границы (левая и правая)
                else
                {
                    Console.SetCursorPosition(startX, currentY);
                    Console.Write(".");

                    Console.SetCursorPosition(endX, currentY);
                    Console.Write(".");
                }

            }
            for (int y = 1; y < tr.radius - 1; y++) // Теперь включаем и последнюю строку
            {
                int startX = temp.CenterX - y + 1; // Начало заливки (на 1 правее левой границы)
                int endX = temp.CenterX + y - 1;   // Конец заливки (на 1 левее правой границы)
                int currentY = temp.CenterY - tr.radius + y; // Смещение по Y
                int smech = 0;

                for (int x = startX; x <= endX; x++) // Заполняем каждый второй символ
                {
                    Console.SetCursorPosition(x, currentY);
                    if (tr.Color == 0) Console.Write(" ");
                    if (tr.Color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                    if (tr.Color == 2) Console.Write("#");
                }
            }

        }

    }
    public void Showinfo()
    {
        for(int i=0;i<list.Count();++i)
        {
            if (list[i] is Circle t )Console.Write($"{i} - Круг {t.radius} ");
            if (list[i] is Triangle tr) Console.Write($"{i} - Треугольник {tr.radius} ");
        }
        Console.WriteLine("");
    }
    public void Change(int param)
    {
        Console.WriteLine("Выберите, в какую сторону сдвинуть фигуру: 1-left, 2-right, 3-up, 4-down, -1 - выход");
        IFigure tmp = list[param];
        list.RemoveAt(param);
        list.Add(tmp);
        while (true)
        {
            Console.SetCursorPosition(0, 5);
            Console.Write("\r"); // Перемещаем курсор в начало строки
            Console.Write(new string(' ', Console.WindowWidth)); // Заполняем строку пробелами
            Console.SetCursorPosition(0, 5);
            int choise = Int32.Parse(Console.ReadLine());
            if (choise == -1) break;
            undraw(list.Count() - 1, true);

            if (choise == 1)
            {
                list[list.Count() - 1].CenterX -= 1;
                for (int i = 0; i < list.Count(); ++i)
                {
                    Redraw(list[i]);
                }
            }
            if (choise == 2)
            {
                list[list.Count() - 1].CenterX += 1;
                for (int i = 0; i < list.Count(); ++i)
                {
                    Redraw(list[i]);
                }
            }
            if (choise == 3)
            {
                list[list.Count() - 1].CenterY -= 1;
                for (int i = 0; i < list.Count(); ++i)
                {
                    Redraw(list[i]);
                }
            }
            if (choise == 4)
            {
                list[list.Count() - 1].CenterY += 1;
                for (int i = 0; i < list.Count(); ++i)
                {
                    Redraw(list[i]);
                }
            }
            
        }
        

    }
    public void Fill(int param)
    {
        IFigure tmp = list[param];
        Console.WriteLine("Введите опцию заливки : 1 - / 2 - #");
        int color = Int32.Parse(Console.ReadLine());

        tmp.Color = color;
        if (tmp is Circle t)
        {
            
            double thickness = 0.2;
            
            for (int y = -t.radius; y <= t.radius; y++)
            {
                for (int x = -t.radius; x <= t.radius; x++)
                {
                    double distance = Math.Sqrt(x * x + y * y); // Вычисляем расстояние от центра
                    double difference = Math.Abs(distance - t.radius); // Отклонение от радиуса

                    if (distance < t.radius - thickness) // Если точка ВНУТРИ круга — меняем символ
                    {
                        Console.SetCursorPosition((t.CenterX + x*2-1), t.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (color == 2) Console.Write("#");
                        Console.SetCursorPosition((t.CenterX + x*2), t.CenterY + y);
                        if (color ==0 ) Console.Write(" ");
                        if (color==1)Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (color == 2) Console.Write("#");
                        Console.SetCursorPosition((t.CenterX + x * 2+1), t.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (color == 2) Console.Write("#");
                    }
                }
            }
            
        }
        if (tmp is Triangle tr)
        {
            for (int y = 1; y < tr.radius-1; y++) // Пропускаем вершину и основание
            {
                int startX = tmp.CenterX - y + 1; // Начало заливки (на 1 правее левой границы)
                int endX = tmp.CenterX + y - 1;   // Конец заливки (на 1 левее правой границы)
                int currentY = tmp.CenterY - tr.radius + y; // Смещение по Y

                for (int x = startX; x <= endX; x++) // Заполняем внутреннюю область
                {
                    Console.SetCursorPosition(x, currentY);
                    if (color == 0) Console.Write(" ");
                    if (color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                    if (color == 2) Console.Write("#");
                }
            }
        }
        list.RemoveAt(param);
        list.Add(tmp);
    }
    };

