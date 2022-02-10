using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkDatabasePlayers
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase[] dataBases =  new DataBase[0];
            string userInput = "";
                        
            while (userInput != "5")
            {
                Console.WriteLine("База данных игроков. Выберите действие.");
                Console.WriteLine(" 1 - Добавить игрока.\n 2 - Удалить игрока.\n 3 - Изменить статус игрока.\n" +
                    " 4 - Список игроков.\n 5 - Выйти из базы данных. ");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1": 
                        AddPlayer(ref dataBases);
                        break;

                    case "2":
                        DeletePlayer( ref dataBases);
                        break;

                    case "3":
                        ChangeStatus(ref dataBases);                                 
                        break;

                    case "4":
                        ShowPlayers(dataBases);
                        break;
                }                
            }                         
        }

        static void AddPlayer(ref DataBase[] dataBases)
        {
            DataBase[] tempDatabases = new DataBase[dataBases.Length + 1];
            for (int i = 0; i<dataBases.Length; i++)
            {
                tempDatabases[i] = dataBases[i];
            }
            tempDatabases[tempDatabases.Length - 1] = new DataBase();
            dataBases = tempDatabases;           
        }

        static void DeletePlayer(ref DataBase[] dataBases)
        {
            if (dataBases.Length > 0)
            {
                Console.WriteLine("Введите порядковый номер игрока для удаления из базы данных");
                string indexPlayer = Console.ReadLine();
                if (int.TryParse(indexPlayer, out int intValue))
                {
                    intValue --;
                    if (intValue < dataBases.Length && intValue >= 0)
                    {
                        DataBase[] tempDatabases = new DataBase[dataBases.Length - 1];

                        for (int i = 0; i < intValue; i++)
                        {
                            tempDatabases[i] = dataBases[i];
                        }

                        for (int i = intValue + 1; i < dataBases.Length; i++)
                        {
                            tempDatabases[i - 1] = dataBases[i];
                        }
                        dataBases = tempDatabases;
                    }
                    else
                    {
                        Console.WriteLine($"Игрок с порядковым номером {intValue + 1} отсутствует.");
                    }                    
                }
                else
                {
                    Console.WriteLine("Не верный порядковый номер игрока");
                }
            }
            else
            {
                Console.WriteLine("База данных не заполнена.");
            }
        }

        static void ChangeStatus(ref DataBase[] dataBases)
        {
            if (dataBases.Length > 0)
            {
                Console.WriteLine("Введите порядковый номер игрока");
                string indexPlayer = Console.ReadLine();
                if (int.TryParse(indexPlayer, out int intValue))
                {
                    intValue--;
                    if (intValue < dataBases.Length && intValue >= 0)
                    {
                        dataBases[intValue].Banned = dataBases[intValue].InputStatusBanned();
                    }
                    else
                    {
                        Console.WriteLine($"Игрок с порядковым номером {intValue + 1} отсутствует.");                        
                    }
                }
                else
                {
                    Console.WriteLine("Не верный порядковый номер игрока");
                }
            }
            else
            {
                Console.WriteLine("База данных не заполнена.");
            }
        }

        static void ShowPlayers(DataBase[] dataBases)
        {
            string statusPlayer;
            for (int i = 0; i < dataBases.Length; i++)
            {
                
                if (dataBases[i].Banned == true)
                {
                   statusPlayer = " - заблокирован";
                }
                else
                {
                    statusPlayer = " - не заблокирован";
                }
                Console.WriteLine($" Порядковый номер {i + 1} | никнейм {dataBases[i].Name} | уровень {dataBases[i].Level}| Статутс игрока {statusPlayer}.");
            }
        }
    }

    class DataBase 
    {
        private int _minLevel = 1;
        private int _maxLevel = 99;
        public string Name { get; private set; }
        public int Level { get; private set; }
        public bool Banned { get;  set; }

        public DataBase()
        {
            Name = InputName();
            Level = InputLevel(_minLevel, _maxLevel);
            Banned = InputStatusBanned();
        }
        
        private string InputName()
        {
            Console.WriteLine("Внесите никнейм для игрока");
            string name = Console.ReadLine();
            return name;
        }

        private int InputLevel(int minLevel, int maxLevel)
        {            
            bool completed = false;
            int intValue = 0;
            Console.WriteLine($"Укажите уровень игрока  от {minLevel} до {maxLevel}");

            while (completed == false)
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out intValue))
                {
                    if (intValue >= minLevel && intValue <= maxLevel)
                    {
                        completed = true;
                    }
                    else
                    {
                        Console.WriteLine($"Не верный ввод значения.Введите целое число от {minLevel} до {maxLevel}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Не верный ввод значения.Введите целое число от {minLevel} до {maxLevel}.");
                }
            }
            return intValue;
        }

        public bool InputStatusBanned()
        {
            bool banned = false;
            bool completed = false;
            string userInput;

            while (completed == false)
            {
                Console.WriteLine("Установите статус игрока.\n 1 - Заблокированый игрок.\n 2 - Разблокированый игрок.");
                userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    banned = true;
                    completed = true;
                }
                else if (userInput == "2")
                {
                    banned = false;
                    completed = true;
                }
            }           
            return banned;            
        }
    }
}
