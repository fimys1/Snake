using System;
using System.Text;
using System.Threading;

namespace Snake
{
    class Program
    {
        
        static SnakeClass a = new SnakeClass();
        static void Main(string[] args)
        {
            a.Start();            
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(20, 12);
            Console.WriteLine("The end");
            Console.ReadKey();
        }
        
    }
}
