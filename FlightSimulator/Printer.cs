using System;

namespace FlightSimulator
{
    //Использование: Printer.PrintColorfulText("Hello, world!", ConsoleColor.Yellow);
    static class Printer
    {
        public static void PrintColorfulText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void BottomLine()
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            Console.CursorTop = Console.WindowTop + Console.WindowHeight - 1;
            // Вспоминаем параметры
            Console.SetCursorPosition(x, y);
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
