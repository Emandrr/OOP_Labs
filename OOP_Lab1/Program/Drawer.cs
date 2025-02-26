using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP1;
class Drawer
{
   
    int x_c = 120;
    int y_c = 32;
    List<IFigure> list = new List<IFigure>();
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
    public void DrawEllipse(int r1,int r2)
    {
        Ellipse temp = new Ellipse();
        temp.CenterX = 120;
        temp.CenterY = 32;
        temp.r1 = r1;
        temp.r2 = r2;
        temp.Color = 0;
        list.Add(temp);
        double thickness = 0.1; // Толщина границы

        for (int y = -r1; y <= r1; y++)
        {
            for (int x = -r2; x <= r2; x++)
            {
                // Формула эллипса: (x² / a²) + (y² / b²) = 1
                double distance = Math.Pow(x / (double)r2, 2) + Math.Pow(y / (double)r1, 2);

                if (Math.Abs(distance - 1) < thickness) // Если точка на границе
                {
                    Console.SetCursorPosition(temp.CenterX + x * 2, temp.CenterY + y);
                    Console.Write(".");
                }
                else if (distance < 1 - thickness) // Если точка внутри эллипса
                {
                    Console.SetCursorPosition(temp.CenterX + x * 2, temp.CenterY + y);
                    Console.Write(" "); // Заполняем внутреннюю часть эллипса
                    Console.SetCursorPosition(temp.CenterX + x * 2+1, temp.CenterY + y);
                    Console.Write(" "); // Заполняем внутреннюю часть эллипса
                    Console.SetCursorPosition(temp.CenterX + x * 2 - 1, temp.CenterY + y);
                    Console.Write(" "); // Заполняем внутреннюю часть эллипса
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
        for (int y = 0; y < height*2; y++) // Перебираем строки треугольника
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
            else if (y == 2*height - 1)
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
        for (int y = 1; y < 2*a - 1; y++) // Теперь включаем и последнюю строку
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
    public void DrawTriangleRect(int a,int b)
    {
        TriangleRect temp = new TriangleRect();
        temp.CenterX = 120;
        temp.CenterY = 32;
        temp.Color = 0;
        temp.a = a;
        temp.b = b;
        list.Add(temp);
        int startX = temp.CenterX - b; // Начало по X
        int startY = temp.CenterY - a; // Начало по Y

        for (int y = 0; y < a*2; y++)
        {
            int currentWidth = (y * b) / a; // Более точный расчет ширины строки

            for (int x = 0; x <= currentWidth*2; x++)
            {
                int drawX = startX + x;
                int drawY = startY + y;

                // Левая катетная линия, гипотенуза и основание
                if (x == 0 || x == currentWidth*2 || y == a*2 - 1)
                {
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(".");
                }
                else
                {
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(" ");
                }
            }
        }
    }
        public void DrawRectangle(int a,int b)
    {
        Rectangle temp = new Rectangle();
        temp.CenterX = 120;
        temp.CenterY = 32;
        temp.Color = 0;
        temp.a = a;
        temp.b = b;
        list.Add(temp);
        int startX = temp.CenterX - b; // Начало по X
        int startY = temp.CenterY - a; // Начало по Y

        for (int y = 0; y < a * 2; y++)
        {
           // int currentWidth = (y * b) / a; // Более точный расчет ширины строки

            for (int x = 0; x < b * 4; x++)
            {
                int drawX = startX + x;
                int drawY = startY + y;

                // Левая катетная линия, гипотенуза и основание
                if (x == 0 || y == a * 2 - 1 || x==4*b-1 || y==0)
                {
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(".");
                }
                else
                {
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(" ");
                }
            }
        }
    }
 
    public void undraw(int param,bool inner_check)
    {
        if(param>list.Count()-1)
        {
            Console.WriteLine("Ошибка ввода: введен неправильный индекс");
            return;

        }
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
            for (int y = 0; y < tr.radius*2; y++) // Перебираем строки треугольника
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
                else if (y == 2*tr.radius - 1)
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
            for (int y = 1; y < 2 * tr.radius - 1; y++) // Теперь включаем и последнюю строку
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
        if (tmp is TriangleRect trRect)
        {
            int startX = trRect.CenterX - trRect.b; // Начало по X
            int startY = trRect.CenterY - trRect.a; // Начало по Y
                                                    // t_rect.Color = color;
            for (int y = 0; y < 2*trRect.a; y++)
            {
                int currentWidth = (y * trRect.b) / trRect.a; // Более точный расчет ширины строки

                for (int x = 0; x <= currentWidth * 2; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    // Левая катетная линия, гипотенуза и основание
                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(" ");
                }
            }
        }
        if(tmp is Ellipse el)
        {
            double thickness = 0.1; // Толщина границы

            for (int y = -el.r1; y <= el.r1; y++)
            {
                for (int x = -el.r2; x <= el.r2; x++)
                {
                    // Формула эллипса: (x² / a²) + (y² / b²) = 1
                    double distance = Math.Pow(x / (double)el.r2, 2) + Math.Pow(y / (double)el.r1, 2);

                    
                    
                    
                        Console.SetCursorPosition(el.CenterX + x * 2, el.CenterY + y);
                        Console.Write(" "); // Заполняем внутреннюю часть эллипса
                    Console.SetCursorPosition(el.CenterX + x * 2+1, el.CenterY + y);
                    Console.Write(" "); // Заполняем внутреннюю часть эллипса
                    Console.SetCursorPosition(el.CenterX + x * 2 - 1, el.CenterY + y);
                    Console.Write(" "); // Заполняем внутреннюю часть эллипса
                }
            }
        }
        if(tmp is Rectangle rec)
        {
            int startX = tmp.CenterX - rec.b; // Начало по X
            int startY = tmp.CenterY - rec.a; // Начало по Y

            for (int y = 0; y < rec.a * 2; y++)
            {
                // int currentWidth = (y * b) / a; // Более точный расчет ширины строки

                for (int x = 0; x < rec.b * 4; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    // Левая катетная линия, гипотенуза и основание
                    
                    
                        Console.SetCursorPosition(drawX, drawY);
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
    public void Redraw(IFigure temp)
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
            for (int y = 0; y < tr.radius*2; y++) // Перебираем строки треугольника
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
                else if (y == 2*tr.radius - 1)
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
            for (int y = 1; y < 2*tr.radius - 1; y++) // Теперь включаем и последнюю строку
            {
                int startX = temp.CenterX - y + 1; // Начало заливки (на 1 правее левой границы)
                int endX = temp.CenterX + y - 1;   // Конец заливки (на 1 левее правой границы)
                int currentY = temp.CenterY - tr.radius + y; // Смещение по Y
              

                for (int x = startX; x <= endX; x++) // Заполняем каждый второй символ
                {
                    Console.SetCursorPosition(x, currentY);
                    if (tr.Color == 0) Console.Write(" ");
                    if (tr.Color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                    if (tr.Color == 2) Console.Write("#");
                }
            }

        }
        if (temp is TriangleRect trRect)
        {
            int startX = trRect.CenterX - trRect.b; // Начало по X
            int startY = trRect.CenterY - trRect.a; // Начало по Y
                                                    // t_rect.Color = color;
            for (int y = 0; y < 2*trRect.a; y++)
            {
                int currentWidth = (y * trRect.b) / trRect.a; // Более точный расчет ширины строки

                for (int x = 0; x <= currentWidth*2; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    if (!(x == 0 || x == currentWidth*2 || y == 2*trRect.a - 1))
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (trRect.Color == 0) Console.Write(" ");
                        if (trRect.Color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (trRect.Color == 2) Console.Write("#");
                    }
                    else
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        Console.Write(".");
                    }
                }
            }
        }
        if (temp is Ellipse el)
        {
            double thickness = 0.1; // Толщина границы

            for (int y = -el.r1; y <= el.r1; y++)
            {
                for (int x = -el.r2; x <= el.r2; x++)
                {
                    // Формула эллипса: (x² / a²) + (y² / b²) = 1
                    double distance = Math.Pow(x / (double)el.r2, 2) + Math.Pow(y / (double)el.r1, 2);

                    if (Math.Abs(distance - 1) < thickness) // Если точка на границе
                    {
                        Console.SetCursorPosition(el.CenterX + x * 2, el.CenterY + y);
                        Console.Write(".");
                    }
                    else if (distance < 1 - thickness) // Если точка внутри эллипса
                    {
                        Console.SetCursorPosition(el.CenterX + x * 2, el.CenterY + y);
                        if (el.Color == 0) Console.Write(" ");
                        if (el.Color == 1) Console.Write("/"); 
                        if (el.Color == 2) Console.Write("#");
                        Console.SetCursorPosition(el.CenterX + x * 2-1, el.CenterY + y);
                        if (el.Color == 0) Console.Write(" ");
                        if (el.Color == 1) Console.Write("/");
                        if (el.Color == 2) Console.Write("#");
                        Console.SetCursorPosition(el.CenterX + x * 2 + 1, el.CenterY + y);
                        if (el.Color == 0) Console.Write(" ");
                        if (el.Color == 1) Console.Write("/");
                        if (el.Color == 2) Console.Write("#");
                    }
                }
            }
        }
        if(temp is Rectangle rec)
        {
            int startX = temp.CenterX - rec.b; // Начало по X
            int startY = temp.CenterY - rec.a; // Начало по Y

            for (int y = 0; y < rec.a * 2; y++)
            {
                // int currentWidth = (y * b) / a; // Более точный расчет ширины строки

                for (int x = 0; x < rec.b * 4; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    // Левая катетная линия, гипотенуза и основание
                    if (x == 0 || y == rec.a * 2 - 1 || x == 4 * rec.b - 1 || y == 0)
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        Console.Write(".");
                    }
                    else
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (rec.Color == 0) Console.Write(" ");
                        if (rec.Color == 1) Console.Write("/");
                        if (rec.Color == 2) Console.Write("#");
                    }
                }
            }
        }


    }
    public void Showinfo()
    {
        for(int i=0;i<list.Count();++i)
        {
            if (list[i] is Circle t )Console.Write($"{i}. - Круг {t.radius} ");
            if (list[i] is Triangle tr) Console.Write($"{i}. - Треугольник {tr.radius} ");
            if (list[i] is TriangleRect trRec) Console.Write($"{i}. - Треугольник прямоугольный  {trRec.a} {trRec.b} ");
            if (list[i] is Ellipse el) Console.Write($"{i}. - Эллипс {el.r1} {el.r2} ");
            if (list[i] is Rectangle rect) Console.Write($"{i}. - Прямоугльник  {rect.a} {rect.b} ");
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
    public void DrawCanva(List<IFigure> list_in)
    {
        for (int i = 10; i < 51; ++i)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("\r"); // Перемещаем курсор в начало строки
            Console.Write(new string(' ', Console.WindowWidth)); // Заполняем строку пробелами
        }
        list = list_in;
        for (int i = 0; i < list.Count(); ++i)
        {
            Redraw(list[i]);
        }
    }
    public void Fill(int param)
    {
        IFigure tmp = list[param];
        Console.WriteLine("Введите опцию заливки : 1 - / 2 - #");
        int color = Int32.Parse(Console.ReadLine());

        
        if (tmp is Circle t)
        {
            t.Color = color;    
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
            tr.Color = color;
            for (int y = 1; y < 2 * tr.radius -1; y++) // Пропускаем вершину и основание
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
        if(tmp is TriangleRect t_rect)
        {
            int startX = t_rect.CenterX - t_rect.b; // Начало по X
            int startY = t_rect.CenterY - t_rect.a; // Начало по Y
            t_rect.Color = color;
            for (int y = 0; y <2* t_rect.a; y++)
            {
                int currentWidth = (y * t_rect.b) / t_rect.a; // Более точный расчет ширины строки

                for (int x = 0; x <= currentWidth*2; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    // Левая катетная линия, гипотенуза и основание
                    if (!(x == 0 || x == currentWidth*2 || y == 2*t_rect.a - 1))
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (color == 2) Console.Write("#");
                    }
                }
            }
        }
        if(tmp is Ellipse el)
        {
            el.Color = color;
            double thickness = 0.1; // Толщина границы
            for (int y = -el.r1; y <= el.r1; y++)
            {
                for (int x = -el.r2; x <= el.r2; x++)
                {
                    // Формула эллипса: (x² / a²) + (y² / b²) = 1
                    double distance = Math.Pow(x / (double)el.r2, 2) + Math.Pow(y / (double)el.r1, 2);

                    
                    if (distance < 1 - thickness) // Если точка внутри эллипса
                    {
                        Console.SetCursorPosition(el.CenterX + x * 2, el.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (color == 2) Console.Write("#");
                        Console.SetCursorPosition(el.CenterX + x * 2-1, el.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (color == 2) Console.Write("#");
                        Console.SetCursorPosition(el.CenterX + x * 2 + 1, el.CenterY + y);
                        if (color == 0) Console.Write(" ");
                        if (color == 1) Console.Write("/"); // Символ внутри круга (можно заменить на любой)
                        if (color == 2) Console.Write("#");
                    }
                }
            }
        }
        if (tmp is Rectangle rec)
        {
            rec.Color = color;
            int startX = tmp.CenterX - rec.b; // Начало по X
            int startY = tmp.CenterY - rec.a; // Начало по Y

            for (int y = 0; y < rec.a * 2; y++)
            {
                // int currentWidth = (y * b) / a; // Более точный расчет ширины строки

                for (int x = 0; x < rec.b * 4; x++)
                {
                    int drawX = startX + x;
                    int drawY = startY + y;

                    // Левая катетная линия, гипотенуза и основание
                    if (!(x == 0 || y == rec.a * 2 - 1 || x == 4 * rec.b - 1 || y == 0))
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        if (rec.Color == 0) Console.Write(" ");
                        if (rec.Color == 1) Console.Write("/");
                        if (rec.Color == 2) Console.Write("#");
                    }
                    
                }
            }
        }
        list.RemoveAt(param);
        list.Add(tmp);
    }
    public List<IFigure> GetData()
    {
        List<IFigure> tmp = list;
        return tmp.ToList();
    }
};

