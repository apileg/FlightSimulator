using System;
using System.Media;

namespace FlightSimulator
{
    class Programm
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(128, 30);
            try
            {
                Plane aircraft = new Plane();
                aircraft.FLightStart();
                SystemSounds.Exclamation.Play();
            }
            catch (ExceptionAirplaneCrushed m)
            {
                Printer.PrintColorfulText($"\n Result: {m.Message}\n", ConsoleColor.Cyan);
                SystemSounds.Exclamation.Play();
                Console.ReadLine();
            }
            catch (ExceptionUnfitToFly m)
            {
                Printer.PrintColorfulText($"\n Result: {m.Message}\n", ConsoleColor.Cyan);
                SystemSounds.Exclamation.Play();
                Console.ReadLine();
            }
            catch (Exception m)
            {
                Printer.PrintColorfulText($"\n {m.Message}\n", ConsoleColor.Cyan);
                SystemSounds.Exclamation.Play();
                Console.ReadLine();
            }
        }
    }
}
