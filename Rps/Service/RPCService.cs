using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using Rps.Service;
using ClassLiberry;
using MyClassLibrary.Models;

namespace RPC.Service
{
    public class RPCService : IRPCService
    {
        public ApplicationDbContext _dbContext { get; set; }

        public RPCService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void PlayGame()
        {
            Console.WriteLine("Välj ditt drag: (1) Sten, (2) Sax, (3) Påse");
            var playerMove = Convert.ToInt32(Console.ReadLine());

            if (playerMove < 1 || playerMove > 3)
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
                return;
            }

            var computerMove = new Random().Next(1, 4);
            var result = DetermineWinner(playerMove, computerMove);

            Console.WriteLine($"Ditt drag: {MoveToString(playerMove)}, Datorns drag: {MoveToString(computerMove)}");
            Console.WriteLine($"Resultat: {result}");

            _dbContext.rpcGames.Add(new Rpc
            {
                PlayerMove = playerMove,
                ComputerMove = computerMove,
                Result = result,
                Date = DateTime.Now
            });
            _dbContext.SaveChanges();
            Console.WriteLine("Spelet har sparats! Tryck på valfri tangent för att fortsätta.");
            Console.ReadLine();
        }

        public void ShowAllGames()
        {
            var games = _dbContext.rpcGames.ToList();
            foreach (var game in games)
            {
                Console.WriteLine($"ID: {game.Id}, Spelare: {MoveToString(game.PlayerMove)}, Dator: {MoveToString(game.ComputerMove)}, Resultat: {game.Result}, Datum: {game.Date}");
            }
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

        private string DetermineWinner(int playerMove, int computerMove)
        {
            if (playerMove == computerMove) return "Oavgjort";

            if (playerMove == 1 && computerMove == 2 || // Sten slår Sax
                playerMove == 2 && computerMove == 3 || // Sax slår Påse
                playerMove == 3 && computerMove == 1)   // Påse slår Sten
            {
                return "Vinst";
            }

            return "Förlust";
        }

        private string MoveToString(int move)
        {
            return move switch
            {
                1 => "Sten",
                2 => "Sax",
                3 => "Påse",
                _ => "Okänt"
            };
        }
    }
}
