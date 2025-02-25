using Lab_OOP1;

void Clear(int k)
{
    for(int i=0;i<k;++i)
    {
        Console.SetCursorPosition(0, 1+i);
        Console.Write("\r"); // Перемещаем курсор в начало строки
        Console.Write(new string(' ', Console.WindowWidth)); // Заполняем строку пробелами
    }
}
Drawer draw = new Drawer();
Console.WriteLine("Введите, что хотите сделать 0 - нарисовать фигуру, 1 - удалить фигуру, 2 - залить фигуру, 3 - сдвинуть фигуру");
while (true)
{

    int parametr;
    Console.SetCursorPosition(0,1);

    parametr = Int32.Parse(Console.ReadLine());
    switch (parametr)
    {
        case 0:
            {
                int k = 0;
                Console.WriteLine("Введите тип фигуры, 0 - круг 1 - треугольник равносторонний 2 - треугольник прямоугольный");
                int param = Int32.Parse(Console.ReadLine());
                switch (param)
                {
                    case 0:
                        {
                            draw.DrawCircle();
                            break;
                        }
                    case 1:
                        {
                            draw.DrawTriangle();
                            break;
                        }
                }
                
                Clear(5);
                break;
            }
        case 1:
            {
                draw.Showinfo();
                draw.undraw(Int32.Parse(Console.ReadLine()),false);
                Clear(4);
                break;

            }
        case 2:
            {
                draw.Showinfo();
                draw.Fill(Int32.Parse(Console.ReadLine()));
                Clear(5);   
                break;
            }
        case 3:
            {
                draw.Showinfo();
                draw.Change(Int32.Parse(Console.ReadLine()));
                Clear(5);
                break;
            }
    }
    if (parametr == -1) break;

}