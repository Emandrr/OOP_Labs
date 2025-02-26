using Lab_OOP1;

void Clear(int k)
{
    for(int i=0;i<k;++i)
    {
        Console.SetCursorPosition(0, 2+i);
        Console.Write("\r"); // Перемещаем курсор в начало строки
        Console.Write(new string(' ', Console.WindowWidth)); // Заполняем строку пробелами
    }
}
Drawer draw = new Drawer();
WorkWithFile worker = new WorkWithFile();
ChangeAction change = new ChangeAction();
Console.WriteLine("Введите, что хотите сделать 0 - нарисовать фигуру, 1 - удалить фигуру, 2 - залить фигуру, 3 - сдвинуть фигуру, 4 - сохранить в файл, 5 - прочесть из файла, CTRL+X - вернуть предыдущее действие, CTRL+Y - отменить возвращение к предыдущему состоянию  ");
//Console.SetCursorPosition(0, 9);
//Console.SetCursorPosition(0, 50);
change.AddToStorageDelete(new List<IFigure>());
bool flag = false;
while (true)
{
    
    int parametr=0;
    Console.SetCursorPosition(0,2);
    string paraam = Console.ReadLine();
   if (paraam == "\u0018" || paraam == "\u0019") // Проверяем, нажата ли клавиша
    {
        Clear(1);
        if (paraam == "\u0018")
        {
            List<IFigure> list = change.Decline();
            if (list is not null) draw.DrawCanva(list);

        }
        else if (paraam == "\u0019")
        {
            List<IFigure> list = change.Accept();
            if (list is not null) draw.DrawCanva(list);

        }
    }

    else
    {


        parametr = Int32.Parse(paraam);

        switch (parametr)
        {
            case 0:
                {
                    int k = 0;
                    Console.WriteLine("Введите тип фигуры, 0 - круг 1 - треугольник равносторонний 2 - треугольник прямоугольный 3 - эллипс 4 - прямоугольник ");
                    int param = Int32.Parse(Console.ReadLine());
                    switch (param)
                    {
                        case 0:
                            {
                                draw.DrawCircle();
                                Clear(5);

                                break;
                            }
                        case 1:
                            {
                                draw.DrawTriangle();
                                Clear(5);

                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Введите длину первой стороны");
                                int a = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("Введите длину второй стороны");
                                int b = Int32.Parse(Console.ReadLine());
                                draw.DrawTriangleRect(a, b);
                                Clear(7);

                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Введите длину радиуса по вертикали");
                                int r1 = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("Введите длину радиуса по горизонтали");
                                int r2 = Int32.Parse(Console.ReadLine());
                                draw.DrawEllipse(r1, r2);
                                Clear(7);

                                break;

                            }
                        case 4:
                            {
                                Console.WriteLine("Введите длину прямоугольника");
                                int a = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("Введите ширину прямоугольника");
                                int b = Int32.Parse(Console.ReadLine());
                                draw.DrawRectangle(a, b);
                                Clear(7);

                                break;
                            }
                    }


                    break;
                }
            case 1:
                {
                    draw.Showinfo();
                    draw.undraw(Int32.Parse(Console.ReadLine()), false);
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
            case 4:
                {
                    worker.WriteInFile(draw.GetData(), "data.json");
                    Clear(5);

                    break;
                }
            case 5:
                {
                    draw.DrawCanva(worker.ReadFromFile("data.json"));
                    Clear(5);

                    break;
                }
        }
        worker.WriteInFile(draw.GetData(), "data1.json");
        change.AddToStorageDelete(worker.ReadFromFile("data1.json"));


        if (parametr == -1) break;
    }
}