using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using Rps.Service;
using ClassLibrary;
using MyClassLibrary.Models;

namespace Rps.Service
{
    public class RPCService : IRPCService
    {
        private int playerWins = 0;
        private int computerWins = 0;
        private int ties = 0;
        public ApplicationDbContext _dbContext { get; set; }

        public RPCService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void PlayGame()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Använd ↑ och ↓ för att välja ett drag och tryck Enter:");
                string[] options = { "[🪨] Sten", "[✂] Sax", "[📜] Påse", "[🚪] Avsluta" };
                int selectedIndex = 0;

                // Arrow key navigation loop
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Välj ditt drag:");

                    for (int i = 0; i < options.Length; i++)
                    {
                        if (i == selectedIndex)
                            Console.WriteLine($"→ {options[i]}");
                        else
                            Console.WriteLine($"  {options[i]}");
                    }

                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.UpArrow)
                        selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                    if (key == ConsoleKey.DownArrow)
                        selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                    if (key == ConsoleKey.Enter)
                        break;
                }

                if (selectedIndex == 3)
                {
                    Console.WriteLine("\nTack för att du spelade!");
                    Console.WriteLine($"🏆 Vinster: {playerWins} | 🤖 Förluster: {computerWins} | 🤝 Oavgjorda: {ties}");
                    return;
                }

                int playerMove = selectedIndex + 1;
                int computerMove = new Random().Next(1, 4);

                Console.Write("\n1... ");
                Thread.Sleep(500);
                Console.Write("2... ");
                Thread.Sleep(500);
                Console.Write("3... ");
                Thread.Sleep(500);
                Console.WriteLine(" SHOOT!\n");

                string result = DetermineWinner(playerMove, computerMove);
                UpdateScore(result);

                // Calculate average win rate
                int totalGames = _dbContext.rpcGames.Count() + 1;
                int totalWins = _dbContext.rpcGames.Count(g => g.Result == "Spelaren vinner!") + (result == "Spelaren vinner!" ? 1 : 0);
                decimal winRate = totalGames > 0 ? (totalWins / (decimal)totalGames) * 100 : 0;

                // Save the game result to the database
                _dbContext.rpcGames.Add(new Rpc
                {
                    PlayerMove = playerMove,
                    ComputerMove = computerMove,
                    Result = result,
                    Date = DateTime.Now,
                    AverageResult = winRate // Store the win rate in the database
                });
                _dbContext.SaveChanges();

                Console.WriteLine($"Ditt drag: {MoveToString(playerMove)} | Datorns drag: {MoveToString(computerMove)}");
                Console.WriteLine($"🔹 Resultat: {result}");
                Console.WriteLine($"\nGenomsnittlig vinstprocent: {winRate:F2}%");

                Console.WriteLine("\nSpelet har sparats! Tryck på valfri tangent för att fortsätta.");
                Console.ReadKey();
            }
        }

        private int ConvertMove(string input)
        {
            return input switch
            {
                "sten" => 1,
                "sax" => 2,
                "påse" => 3,
                _ => 0
            };
        }

        private string MoveToString(int move)
        {
            return move switch
            {
                1 => "🪨 Sten",
                2 => "✂️ Sax",
                3 => "📜 Påse",
                _ => "Okänt"
            };
        }
      
        private string DetermineWinner(int playerMove, int computerMove)
        {
            if (playerMove == computerMove) return "Oavgjort";

            if ((playerMove == 1 && computerMove == 2) ||  // Sten slår Sax
                (playerMove == 2 && computerMove == 3) ||  // Sax slår Påse
                (playerMove == 3 && computerMove == 1))    // Påse slår Sten
            {
                return "Spelaren vinner!";
            }

            return "Datorn vinner!";
        }
        private void UpdateScore(string result)
        {
            if (result == "Spelaren vinner!") playerWins++;
            else if (result == "Datorn vinner!") computerWins++;
            else ties++;
        }
        public void ShowAllGames()
        {
            var games = _dbContext.rpcGames.ToList();
            if (!games.Any())
            {
                Console.WriteLine("Inga spel har spelats ännu.");
                return;
            }

            Console.WriteLine("=== Spelhistorik ===");
            foreach (var game in games)
            {
                Console.WriteLine($"ID: {game.Id}, Spelare: {MoveToString(game.PlayerMove)}, Dator: {MoveToString(game.ComputerMove)}, Resultat: {game.Result}, Datum: {game.Date}, Genomsnittlig vinstprocent: {game.AverageResult:F2}%");
            }

            decimal latestWinRate = games.Last().AverageResult;
            Console.WriteLine($"\n🔹 Senaste uppdaterade genomsnittliga vinstprocent: {latestWinRate:F2}%");
        }

        public void DeleteGameById()
        {
            ShowAllGames();
            Console.WriteLine("Ange ID för spelet du vill ta bort:");
            var id = Convert.ToInt32(Console.ReadLine());
            var game = FindGameById(id);
            if (game != null)
            {
                _dbContext.rpcGames.Remove(game);
                _dbContext.SaveChanges();
                Console.WriteLine("Spelet har tagits bort.");
            }
            else
            {
                Console.WriteLine("Spelet hittades inte.");
            }
        }

        public void UpdateGameById()
        {
            ShowAllGames();
            Console.WriteLine("Ange ID för spelet du vill uppdatera:");
            var id = Convert.ToInt32(Console.ReadLine());
            var game = FindGameById(id);
            if (game != null)
            {
                Console.WriteLine("Ange nytt drag för spelaren: (1) Sten, (2) Sax, (3) Påse");
                game.PlayerMove = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Ange nytt drag för datorn: (1) Sten, (2) Sax, (3) Påse");
                game.ComputerMove = Convert.ToInt32(Console.ReadLine());

                game.Result = DetermineWinner(game.PlayerMove, game.ComputerMove);
                game.Date = DateTime.Now;

                _dbContext.SaveChanges();
                Console.WriteLine("Spelet har uppdaterats.");
            }
            else
            {
                Console.WriteLine("Spelet hittades inte.");
            }
        }

        public Rpc FindGameById(int id)
        {
            var game = _dbContext.rpcGames.Find(id);
            if (game != null)
            {
                Console.WriteLine($"ID: {game.Id}, Spelare: {MoveToString(game.PlayerMove)}, Dator: {MoveToString(game.ComputerMove)}, Resultat: {game.Result}, Datum: {game.Date}");
            }
            else
            {
                Console.WriteLine("Spelet hittades inte.");
            }
            return game;
        }

    
    }
}
