using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BattleShip.Domain;

namespace BattleShip.TestConsole
{
    class Program
    {
        private static GameEngine.GameEngine gameEngine = new GameEngine.GameEngine();

        static void Main(string[] args)
        {
            AddAccount();
        }

        private static void AddAccount()
        {
            try
            {
                gameEngine.NewAccount("WrickedGamer", "Test123", "wrickedgamer@example.com");
                gameEngine.NewAccount("Chibi", "Test123", "chibi@example.com");

                Console.WriteLine("Accounts added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //private static void Login()
        //{
        //    try
        //    {
        //        Task<Account> account = gameEngine.Login("WrickedGamer", "Test123");
        //        Console.WriteLine("Account logged in!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void GetAccount()
        //{
        //    try
        //    {
        //        Task<Account> account = gameEngine.GetAccount(1);

        //        if(account != null)
        //        {
        //            Console.WriteLine(account.Name);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void AddGame()
        //{
        //    try
        //    {
        //        Task<string> gameKey = gameEngine.NewGame(1, true);
        //        Console.WriteLine("New game added with gamekey: " + gameKey.Result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void AddGame()
        //{
        //    try
        //    {
        //        Task<string> gameKey = gameEngine.NewGame(1, true);
        //        Console.WriteLine("New game added with gamekey: " + gameKey.Result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void JoinGame()
        //{
        //    try
        //    {
        //        gameEngine.JoinGame("abc123", 2);
        //        Console.WriteLine("You have join game!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void GameKeyExist()
        //{
        //    try
        //    {
        //        bool result = gameEngine.GameKeyExist("abc123");

        //        if (result)
        //        {
        //            Console.WriteLine("GameKey exist!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("GameKey doesn't exist!");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void GetGame()
        //{
        //    try
        //    {
        //        Task<GameBoard> game = gameEngine.GetGame("abc123");
        //        Console.WriteLine(String.Format("Game {0} started by {1}", game.Result.GameKey, game.Result.Players.ElementAt(1).Name);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void GetOpenGames()
        //{
        //    try
        //    {
        //        Task<List<GameBoard>> games = gameEngine.GetOpenGames();

        //        Console.WriteLine("Existing open games:");

        //        foreach (GameBoard game in games.Result)
        //        {
        //            Console.WriteLine("\t" + game.GameKey);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void GetAccountGames()
        //{
        //    try
        //    {
        //        Task<List<GameBoard>> games = gameEngine.GetAccountGames(1);

        //        Console.WriteLine("Account games:");

        //        foreach (GameBoard game in games.Result)
        //        {
        //            Console.WriteLine("\t" + game.GameKey);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void Shoot()
        //{
        //    try
        //    {
        //        bool result = gameEngine.Shoot(1, "ABC123", 1, 1);

        //        Console.WriteLine("Shoot at position 1,1 on player 1 - Result:" + result.ToString());

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private static void RemoveOldGames()
        //{
        //    try
        //    {
        //        gameEngine.RemoveOldGameBoard();

        //        Console.WriteLine("Removed old gameboards!");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}
