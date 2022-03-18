using System;
using System.Media;

// Если разница в диапазоне от 300 до 600 то пилот получает 25 штрафных очков, если от 600 до 1000 — то 50 очков. 
// Если разница превышает 1000, то объект-диспетчер генерирует исключительную ситуацию «Самолет раз бился», 
// которая должна быть обработана при ложе нием, как прекращение тренировочного полета с соот ветст вую щей информацией на экране. 
// Если пилот, не завершив полет, набирает 1000 штрафных очков от любого диспетчера— то этот объект диспетчер генерирует исключительную ситуацию «Непригоден кполетам», 
// которая также обрабатывается приложением. 

namespace FlightSimulator
{
    class Dispatcher
    {
        public string Name { get; set; }
        public int FineScore { get; private set; } // Штрафные очки

        private int SettingWeatherConditions; // Установка погодных условий

        //То, что у констант одинаковые значения не значит, что они должны быть записаны, как одна константа.
        private const int MaxSpeed = 1000;
        private const int MaxHeightDifference = 1000;
        private const int MaxFineScore = 1000;

        public Dispatcher(string Name)
        {
            this.Name = Name;

            Random RandN = new Random();
            SettingWeatherConditions = (RandN.Next(-200, 200));

            FineScore = 0;
        }

        public void ControlFlight(int CurrentSpeed, int CurrentHeight)
        {           
            int RecomendedHeight = 7 * CurrentSpeed - SettingWeatherConditions;
            int HeightDifference = Math.Abs(CurrentHeight - RecomendedHeight);

            Printer.PrintColorfulText($"\n Dispatcher - {Name}: Recommended height is {RecomendedHeight}.\n", ConsoleColor.Red);

            CheckHeightDifference(HeightDifference);
            CheckSpeed(CurrentSpeed);
            CheckFineScore();        
        }

        private void CheckHeightDifference(int HeightDifference)
        {
            if (HeightDifference >= 300 && HeightDifference < 600)
            {
                FineScore += 25;
            }
            else if (HeightDifference >= 600 && HeightDifference <= MaxHeightDifference)
            {
                FineScore += 50;
            }
            else if (HeightDifference >= MaxHeightDifference)
            {
                Console.Write($"\n Dispathcer - {Name}: Ohh... Airplane crashed. You have failed your exams. Try again!\n");
                Printer.BottomLine();
                throw new ExceptionAirplaneCrushed($"\n The reason is the difference between current height" + 
                                                   $" and recomended height - ({HeightDifference})");
            }
        }

        private void CheckSpeed(int CurrentSpeed)
        {
            if (CurrentSpeed > MaxSpeed)
            {
                FineScore += 100;

                SystemSounds.Exclamation.Play();
                SystemSounds.Exclamation.Play();

                Printer.PrintColorfulText($"\n Dispathcer - {Name}: Caution! Slow down the plane. Your speed: {CurrentSpeed}.\n", 
                    ConsoleColor.Red);
            }
            else if (CurrentSpeed >= 750)
            {
                Printer.PrintColorfulText($"\n Dispathcer - {Name}: You are about to exceed the speed!.\n", ConsoleColor.Yellow);
            }
        }

        private void CheckFineScore()
        {
            if (FineScore >= MaxFineScore)
            {
                Console.Write($"\n Dispathcer - {Name}: You broke the rules. You have failed your exams. Try again!");
                Printer.BottomLine();
                throw new ExceptionUnfitToFly(" Unfit to fly\n");
            }
        }
    }
}
