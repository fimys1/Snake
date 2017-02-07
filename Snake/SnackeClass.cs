using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class SnakeClass
{
    int y = 7, x = 7,time=400;
    bool Left = true, Right = true, Up = true, Down = true, exit=false;
    public int[] p = { 0, 0 };
    public int sizeSnake = 0;
    private ConsoleKeyInfo e, eCopy;
    List<Thread> listthread = new List<Thread>();
    public Thread ThreadFeald, ThreadControl, ThreadWrite,list;
    Random rValue = new Random();
    public char[,] box = new char[15, 15];
    public char[,] boxHelp = new char[15, 15];
    public int[] arrey = new int[] { 0, 0 };
    public SnakeClass()
    {
        Console.CursorVisible = false;
        ThreadWrite = new Thread(ReadKay);
        ThreadFeald = new Thread(printFealdSnake);
        ThreadControl = new Thread(Control);
        for (int i = 0; i <= 14; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                box[i, j] = ' ';
            }
        }
        for (int x = 0; x <= 14; x++)
        {
            box[x, 0] = 'X';
            box[0, x] = 'X';
            box[14, x] = 'X';
            box[x, 14] = 'X';
        }
        box[7, 7] = '*';
        Console.InputEncoding = Encoding.Unicode;
        Console.Clear();
        printFeald();
    }

    volatile int starterCount = 0;
    object LockObject = new object();
    ManualResetEvent startEvent = new ManualResetEvent(false);

    void Starting(object paramThread)
    {
        lock (LockObject)
        {
            starterCount++;
        }
        startEvent.WaitOne();
        (paramThread as Thread).Start();
    }

    public void Start()
    {
        listthread.Add(ThreadWrite);
        listthread.Add(ThreadFeald);
        listthread.Add(ThreadControl);
        foreach (var thread in listthread)
            new Thread(Starting).Start(thread);
        while (starterCount < 3) Thread.Sleep(1);
        Thread.Sleep(100);
        startEvent.Set();
        do
        {
            chekPositionSnake();
            Thread.Sleep(50);
        }
        while (!exit);
        Stop();
    }

    public void Stop()
    {
        ThreadWrite.Abort();
        ThreadFeald.Abort();
        ThreadControl.Abort();
    }

    public bool size()
    {
        bool check = false;
        if (sizeSnake >= 169)
        {
            check = true;
        }
        return check;
    }

    public void chekPositionSnake() //проверка позиции змеи с её кормом
    {
        if (size())
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(20, 11);
            Console.WriteLine("You are win!!!");
            exit = true;
        }
        else if (x == p[0] && y == p[1])
        {
            sizeSnake++;       
            do
            {
                changer();
            }
            while(box[p[0],p[1]]!=' ');
            box[p[0], p[1]] = Convert.ToChar('!');
        }
    }

    private void sizeSnakeEver() 
    {        
        if (sizeSnake > 0)
        {
            delete();
            for (int i = sizeSnake; 0 < i; i--)
            {
                if (i > 1)
                {
                    finder(Convert.ToChar(i - 1 + 96));
                    box[arrey[0], arrey[1]] = Convert.ToChar(i + 96);
                }
                else
                {
                    finder('*');
                    box[arrey[0], arrey[1]] = Convert.ToChar(1 + 96);
                }
            }
        }
    }

    private void printFeald()
    {
        for (int j = 0; j <= 14; j++)
        {

            for (int i = 0; i <= 14; i++)
            {
                if (box[i, j] == 'X')
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(box[i, j]);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(box[i, j]);
                }
            }
            Console.WriteLine();
        }
        changer();
        box[p[0], p[1]] = '!';
        Array.Copy(box, boxHelp, box.Length);
    } // отрисовка поля при старте программы

    private void printFealdSnake()
    {
        while (true)
        {
            for (int i = 0; i <= 14; i++)
            {
                for (int j = 0; j <= 14; j++)
                {
                    if (box[i, j] == '*')
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(i, j);
                        Console.Write('*');
                    }
                    else if (box[i, j] == '!')
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(i, j);
                        Console.Write('!');
                    }
                    else if (box[i, j] == ' ')// || box[i, j] == 0
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(i, j);
                        Console.Write(box[i, j]);
                    }
                    else if (box[i, j] == 'X')
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(i, j);
                        Console.Write(box[i, j]);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(i, j);
                        Console.Write('+');
                    }

                }
            }
        }
    } //постоянная отрисовка змеи и корма

    private void Control() //передвижение змеи по массиву
    {
        while (true)
        {
            Array.Copy(box, boxHelp, box.Length);
            sizeSnakeEver();
            if (eCopy.Key == ConsoleKey.LeftArrow)
            {
                if ((x - 1) == 0 || box[x - 1, y] != ' ' && box[x - 1, y] != '!')
                {
                    exit = true;
                }
                box[x - 1, y] = '*';
                if (sizeSnake == 0)
                {
                    box[x, y] = ' ';
                }
                x = x - 1;
            }
            else if (eCopy.Key == ConsoleKey.RightArrow)
            {
                if ((x + 1) == 14 || box[x + 1, y] != ' ' && box[x + 1, y] != '!')
                {
                    exit = true;
                }
                box[x + 1, y] = '*';
                if (sizeSnake == 0)
                {
                    box[x, y] = ' ';
                }
                x = x + 1;
            }
            else if (eCopy.Key == ConsoleKey.UpArrow)
            {
                if ((y - 1) == 0 || box[x, y - 1] != ' ' && box[x, y - 1] != '!')
                {
                    exit = true;
                }
                box[x, y - 1] = '*';
                if (sizeSnake == 0)
                {
                    box[x, y] = ' ';
                }
                y = y - 1;
            }
            else if (eCopy.Key == ConsoleKey.DownArrow)
            {
                if ((y + 1) == 14 || box[x, y + 1] != ' ' && box[x, y + 1] != '!')
                {
                    exit = true;
                }
                box[x, y + 1] = '*';
                if (sizeSnake == 0)
                {
                    box[x, y] = ' ';
                }
                y = y + 1;
            }
            //printFealdSnake();
            //chekPositionSnake();
            //sizeSnake++;
            Thread.Sleep(time);
        }
    }

    private void ReadKay()
    {
        //do
        while (true)
        {
            e = Console.ReadKey(true);
            if (e.Key == ConsoleKey.LeftArrow && Left == true)
            {
                eCopy = e;           
                Right = false;
                Up = true;
                Down = true;
                Left = true;
            }
            else if (e.Key == ConsoleKey.RightArrow && Right == true)
            {
                eCopy = e;
                Right = true;
                Left = false;
                Up = true;
                Down = true;
            }
            else if (e.Key == ConsoleKey.UpArrow && Up == true)
            {
                eCopy = e;
                Right = true;
                Left = true;
                Up = true;
                Down = false;
            }
            else if (e.Key == ConsoleKey.DownArrow && Down == true)
            {
                eCopy = e;
                Right = true;
                Left = true;
                Up = false;
                Down = true;
            }
            //e = Console.ReadKey(false);
            Thread.Sleep(0);
        }
        //while (exit == false);
        
    } //запуск цыкла для передвижения головы змеи

    public void changer()
    {
        p[0] = rValue.Next(1, 12);
        p[1] = rValue.Next(1, 12);
    }

    public void finder(char a)
    {
        for (int i = 1; i < 14; i++)
        {
            for (int j = 1; j < 14; j++)
            {
                if (boxHelp[i, j] == a)
                {
                    arrey[0] = i;
                    arrey[1] = j;
                }
            }
        }
    }

    private void delete()
    {
        for (int i = 1; i < 14; i++)
        {
            for (int j = 1; j < 14; j++)
            {
                if (box[i, j] != '*' && box[i, j] != '!')
                {
                    box[i, j] = ' ';
                }
            }
        }
    }
}
