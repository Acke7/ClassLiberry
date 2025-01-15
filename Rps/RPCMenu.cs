using Rps.Service;
using System;

namespace Rps
{
    public class RPCMenu
    {
        private readonly IRPCService _rpcService;

        public RPCMenu(IRPCService rpcService)
        {
            _rpcService = rpcService;
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                string[] options = {
                    "1. Spela Sten, Sax, Påse",
                    "2. Visa tidigare spel",
                    "3. Ta bort ett spel",
                    "4. Uppdatera ett spel",
                    "5. Avsluta"
                };

                int choice = DisplayMenu("Sten, Sax, Påse", options);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Du valde att spela ett nytt spel.");
                        _rpcService.PlayGame();
                        break;
                    case 2:
                        Console.WriteLine("Du valde att visa tidigare spel.");
                        _rpcService.ShowAllGames();
                        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Du valde att ta bort ett spel.");
                        _rpcService.DeleteGameById();
                        break;
                    case 4:
                        Console.WriteLine("Du valde att uppdatera ett spel.");
                        _rpcService.UpdateGameById();
                        break;
                    case 5:
                        Console.WriteLine("Avslutar... Tack för att du spelade!");
                        return;
                }
            }
        }

        int DisplayMenu(string title, string[] options)
        {
            int currentIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"--- {title} ---\n");

                // Display options with hover effect
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentIndex = currentIndex == 0 ? options.Length - 1 : currentIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentIndex = currentIndex == options.Length - 1 ? 0 : currentIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        return currentIndex + 1;
                }
            }
        }
    }
}
