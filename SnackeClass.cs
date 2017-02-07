using System;

public class SnakeClass
{
    public SnakeClass()
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(9, 9);
        Console.Write(" ");
        Console.SetCursorPosition(9, 9);
    }
    public SnakeClass(ConsoleKeyInfo muve) : this(SnakeClass)
    {
        if (muve == ConsoleKey.LeftArrow)
        {
            mov();
        }
    }
    private void mov(int g)
    {
        for (int i = 1; i < 2; i++)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            //      Console.CursorVisible = true;
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" ");
            Thread.Sleep(1000);
        }
    }

}
