using System;
using System.Timers;

// Метод работы. 
// Создается экземпляр Timer объекта ( SystemTimer ), который подписывает и запускает Timer.Elapsed событие ( SystemTimer.Elapsed += TimeEvent ).
// И каждые 2 секунды ( System.Timers.Timer(2000) ) настраивает обработчик ивентов для события и запускает таймер.
// Обработчик событий ( ElapsedEventArgs e ) отображает значение, получает дату и время возникновения события Elapsed. 


namespace FlightSimulator
{
    class Timer
    {
        // Создает событие после заданного интервала с возможностью создания повторяющихся событий.
        private static System.Timers.Timer SystemTimer;
        public static void SetTimer()
        {
            // Создайте таймер с двухсекундным интервалом.
            SystemTimer = new System.Timers.Timer(2000);
            // Подключите событие Elapsed к таймеру.
            SystemTimer.Elapsed += TimeEvent;
            // Автообновление
            SystemTimer.AutoReset = true;
            // Включаем таймер
            SystemTimer.Enabled = true;
        }

        public static void TimeEvent(Object source, ElapsedEventArgs eva)
        {
            int left = Console.CursorLeft; // Запоминаем параметры
            int top = Console.CursorTop;  //  Запоминаем параметры

            Console.SetCursorPosition(Console.WindowWidth - 1 - 12, Console.WindowTop);
            Printer.PrintColorfulText($"------------", ConsoleColor.Yellow);
            Console.SetCursorPosition(Console.WindowWidth - 1 - 12, Console.WindowTop + 1);
            Printer.PrintColorfulText($"|[{DateTime.Now.ToString("hh:mm:ss")}]|", ConsoleColor.Yellow);
            Console.SetCursorPosition(Console.WindowWidth - 1 - 12, Console.WindowTop + 2);
            Printer.PrintColorfulText($"------------", ConsoleColor.Yellow);

            Console.SetCursorPosition(left, top); // Вспоминаем параметры
            Printer.ClearCurrentConsoleLine();
        }
    }
}
