using System;
using System.Collections.Generic;
using System.Media;

namespace FlightSimulator
{
    class Plane
    {
        private const int Max = 1000;
        private int CurrentSpeed; // Текущая скорость
        private int CurrentHeight; // Текущая высота
        private int TotalFineScore; // Общая сумма штрафов
        private bool HasReachedTheMax; // Набрана ли максимальная высота
        private bool HasTheFlightStarted; // Начался ли полет
        private List<Dispatcher> DispatchersList; // Общее количество диспетчеров

        private delegate void ChangeDelegate(int Speed, int Height); // Делегат изменений
        private event ChangeDelegate ChangeEvent; // События для делегата изменений

        public Plane() // Конструктор
        {
            DispatchersList = new List<Dispatcher>(); // Создаем диспетчера
            CurrentHeight = 0;
            CurrentSpeed = 0;
            TotalFineScore = 0;
            HasReachedTheMax = false;
            HasTheFlightStarted = false;
        }
        public void AddDispatcherInList(string Name)
        {
            Dispatcher dispatcher = new Dispatcher(Name);
            ChangeEvent += dispatcher.ControlFlight; // Подписываем на событие для дальнейшых действий
            DispatchersList.Add(dispatcher); // Добавляем в список
            Console.Write($"\n Dispathcer - {Name} is added!\n");
        }
        public void DeletDispatcherFromList(int PositionInList)
        {
            if (PositionInList >= 0 && PositionInList <= DispatchersList.Count - 1)
            {
                ChangeEvent -= DispatchersList[PositionInList].ControlFlight; // Отписываемся от события 
                DispatchersList.RemoveAt(PositionInList);
                Console.Write($"\n Dispatcher - {DispatchersList[PositionInList].Name} deleted.\n"); // Удаляем диспетчера 
                TotalFineScore += DispatchersList[PositionInList].FineScore;
            }
            else
            {
                Console.Write($"\n Dispatcher does not exist.\n");
            }
        }

        public void CallDispatcher() // Звоним диспетчеру и вызываем его
        {
            Printer.PrintColorfulText("\n 0. Cancel.\n",ConsoleColor.Magenta);
            foreach (Dispatcher item in DispatchersList)
            {
                Printer.PrintColorfulText($"{DispatchersList.IndexOf(item) + 1}. {item.Name}\n", ConsoleColor.Cyan);
            }
        }
        public void CheckSpeedAndHeigh(bool flag,int a) // Проверка input.Key 
        {
            if (flag == false)
            {
                if (HasTheFlightStarted)
                {
                    CurrentHeight -= a;
                }
                else
                {
                    CurrentHeight = 0;
                    SystemSounds.Exclamation.Play();
                    Printer.PrintColorfulText($"\n An airplane cannot take off without accelerating.\n", ConsoleColor.Yellow);
                }
            }
            else if (flag == true)
            {
                if (HasTheFlightStarted)
                {
                    CurrentHeight += a;
                }
                else
                {
                    CurrentHeight = 0;
                    SystemSounds.Exclamation.Play();
                    Printer.PrintColorfulText($"\n An airplane cannot take off without accelerating.\n", ConsoleColor.Yellow);
                }
            }
        }
        public void FLightStart()
        {
            Printer.PrintColorfulText($" --------------------------------------------------------------------------------------------------------------\n", ConsoleColor.Green);
            Printer.PrintColorfulText($" |The pilot's task is to take off on the plane, gain the maximum (1000 km / h) speed, and then land the plane.|\n",ConsoleColor.Green);
            Printer.PrintColorfulText($" --------------------------------------------------------------------------------------------------------------", ConsoleColor.Green);
            //Timer.SetTimer();
            Console.Write(
                          $" \n Control: " +
                          $" \n > (+) (=) - Add new despatcher; " +
                          $" \n > (-) Delete despatcher; " +
                          $" \n > RightArrow - Speed up the plane by 50; " +
                          $" \n > LeftArrow - Reduce the plane speed by 50;" +
                          $" \n > Control + RightArrow - Speed up the plane by 150;" +
                          $" \n > Control + LeftArrow - Reduce the plane speed by 150;" +
                          $" \n > UpArrow - Height up the plane by 500; " +
                          $" \n > DownArrow - Reduce height the plane by 250;" +
                          $" \n > Control + UpArrow - Height up the plane by 500;" +
                          $" \n > Control + DownArrow - Reduce height the plane by 500; \n"
                          );

            ConsoleKeyInfo input; // https://docs.microsoft.com/en-us/dotnet/api/system.consolekeyinfo?view=netcore-3.1 
                                 // Описывает нажатую клавишу консоли, включая символ, представленный клавишей консоли, и состояние клавиш-модификаторов SHIFT, ALT и CTRL. 
            while (true)
            {
                input = Console.ReadKey(true);

                if ((input.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    if (input.Key == ConsoleKey.RightArrow)
                        CurrentSpeed += 150;
                    else if (input.Key == ConsoleKey.LeftArrow)
                        CurrentSpeed -= 150;
                    else if (input.Key == ConsoleKey.UpArrow)
                        CheckSpeedAndHeigh(true, 500);
                    else if (input.Key == ConsoleKey.DownArrow)
                        CheckSpeedAndHeigh(false, 500);
                }
                else
                {
                    if (input.Key == ConsoleKey.RightArrow)
                        CurrentSpeed += 50;
                    else if (input.Key == ConsoleKey.LeftArrow)
                        CurrentSpeed -= 50;
                    else if (input.Key == ConsoleKey.DownArrow)
                        CheckSpeedAndHeigh(false, 250);
                    else if (input.Key == ConsoleKey.UpArrow)
                        CheckSpeedAndHeigh(true, 250);
                    else if (input.Key == ConsoleKey.OemPlus || input.Key == ConsoleKey.Add)
                    {
                        Console.Write($"\n Enter name of dispatcher: ");
                        AddDispatcherInList(Console.ReadLine());
                        Console.WriteLine();
                    }
                    else if (input.Key == ConsoleKey.OemMinus || input.Key == ConsoleKey.Subtract)
                    {
                        CallDispatcher();
                        Console.Write($"\n Enter index of dispatcher, that want to delete: ");
                        DeletDispatcherFromList(Convert.ToInt32(Console.ReadLine()) - 1); // -1 для правильной индексации 
                        Console.WriteLine();
                    }
                    if (DispatchersList.Count >= 2 && CurrentSpeed >= 50) // Начало работы диспетчера
                    {
                        if (!HasTheFlightStarted)
                        {
                            Printer.PrintColorfulText($"\n The flight has started\n",ConsoleColor.Green);
                            HasTheFlightStarted = true;
                        }
                        ChangeEvent(CurrentSpeed, CurrentHeight); // Для сообщения изменений в показателях
                        if (CurrentSpeed == Max)
                        {
                            HasReachedTheMax = true;
                            Printer.PrintColorfulText($"\n You have reached the maximum speed, it remains only to land the plane!\n",ConsoleColor.Green); // Набрана макс. скорость, осталось посадить самолет
                        }
                        else if (HasReachedTheMax && CurrentSpeed <= 50)
                        {
                            Console.Write("\n The flight is over. \n");

                            foreach (Dispatcher item in DispatchersList) // Суммируем штрафы всех диспетчеров
                            {
                                TotalFineScore += item.FineScore;
                                Console.Write($"\n {item.Name} - {item.FineScore}\n");
                            }
                            Printer.PrintColorfulText($"\n Amount of fine is {TotalFineScore}\n", ConsoleColor.Green);

                            break;
                        }
                    }
                }
                if (DispatchersList.Count >= 2)
                {
                    if ((CurrentHeight <= 0) && (CurrentSpeed <= 0))
                    {
                        CurrentHeight = 0; CurrentSpeed = 0;
                    }
                    Console.Write($"\n Current speed: {CurrentSpeed} km/h ; Current height {CurrentHeight} m \n");
                    //Timer.SetTimer();
                }
            }
        }
    }
}
