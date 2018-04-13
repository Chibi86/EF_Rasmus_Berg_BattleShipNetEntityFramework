using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Domain;

namespace BattleShip.TestConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            //AddAccount();
            Login();
        }

        private static void AddAccount()
        {
            try
            {
                GameEngine.GameEngine.NewAccount("WrickedGamer", "Test123", "wrickedgamer@example.com");
                GameEngine.GameEngine.NewAccount("Chibi", "Test123", "chibi@example.com");

                Console.WriteLine("Accounts added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Login()
        {
            try
            {
                /*Task<*/Account/*>*/ account = GameEngine.GameEngine.Login("WrickedGamer", "Test1234");
                Console.WriteLine("Account " + account/*.Result*/.UserName + " now logged in!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //private static void GetAccount()
        //{
        //    try
        //    {
        //        Task<Account> account = GameEngine.GameEngine.GetAccount(1);

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
        //        Task<string> gameKey = GameEngine.GameEngine.NewGame(1, true);
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
        //        Task<string> gameKey = GameEngine.GameEngine.NewGame(1, true);
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
        //        GameEngine.GameEngine.JoinGame("abc123", 2);
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
        //        bool result = GameEngine.GameEngine.GameKeyExist("abc123");

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
        //        Task<GameBoard> game = GameEngine.GameEngine.GetGame("abc123");
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
        //        Task<List<GameBoard>> games = GameEngine.GameEngine.GetOpenGames();

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
        //        Task<List<GameBoard>> games = GameEngine.GameEngine.GetAccountGames(1);

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
        //        bool result = GameEngine.GameEngine.Shoot(1, "ABC123", 1, 1);

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
        //        GameEngine.GameEngine.RemoveOldGameBoard();

        //        Console.WriteLine("Removed old gameboards!");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}
