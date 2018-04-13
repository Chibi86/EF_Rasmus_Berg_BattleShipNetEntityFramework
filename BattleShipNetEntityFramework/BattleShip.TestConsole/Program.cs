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
            //Login();
            //GetAccount();
            //AddGame();
            //JoinGame();
            //GameKeyExist();
            //GetGame();
            //GetOpenGames();
            //GetAccountGames();
            //Shoot();
            RemoveOldGames();
        }

        private static void AddAccount()
        {
            try
            {
                GameEngine.GameEngine.NewAccount("WrickedGamer2", "Test123", "wrickedgamer@example.com");
                GameEngine.GameEngine.NewAccount("Chibi2", "Test123", "chibi@example.com");

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
                Account account = GameEngine.GameEngine.Login("WrickedGamer", "Test123");
                Console.WriteLine("Account " + account.UserName + " now logged in!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetAccount()
        {
            try
            {
                Account account = GameEngine.GameEngine.GetAccount(10);

                if (account != null)
                {
                    Console.WriteLine("Got account with username: " + account.UserName);
                }
                else
                {
                    Console.WriteLine("No user with that id!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddGame()
        {
            try
            {
                string gameKey = GameEngine.GameEngine.NewGame(1, true);
                Console.WriteLine("New game added with gamekey: " + gameKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void JoinGame()
        {
            try
            {
                GameEngine.GameEngine.JoinGame("a239ce", 2);
                Console.WriteLine("You have join game!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GameKeyExist()
        {
            try
            {
                bool result = GameEngine.GameEngine.GameKeyExist("a239ce");

                if (result)
                {
                    Console.WriteLine("GameKey exist!");
                }
                else
                {
                    Console.WriteLine("GameKey doesn't exist!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetGame()
        {
            try
            {
                GameBoard game = GameEngine.GameEngine.GetGame("a239ce");
                if (game != null)
                {
                    Console.WriteLine(String.Format("Game {0} started by {1}", game.Key, game.Players.ElementAt(0).Account.UserName));
                }
                else
                {
                    Console.WriteLine("GameBoard with this key doesn't exist!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetOpenGames()
        {
            try
            {
                List<GameBoard> games = GameEngine.GameEngine.GetOpenGames();

                Console.WriteLine("Existing open games:");

                foreach (GameBoard game in games)
                {
                    Console.WriteLine("\t" + game.Key + " started of " + game.Players.ElementAt(0).Account.UserName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetAccountGames()
        {
            try
            {
                List<GameBoard> games = GameEngine.GameEngine.GetAccountGames(1);

                Console.WriteLine("WrickedGamer's games:");

                foreach (GameBoard game in games)
                {
                    Console.WriteLine("\t" + game.Key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Shoot()
        {
            try
            {
                //bool result = GameEngine.GameEngine.Shoot(25, "f9f0cb", 1, 1);
                bool result = GameEngine.GameEngine.Shoot(27, "f9f0cb", 1, 1);

                Console.WriteLine("Shoot at position 1,1 on player 1 - Result:" + result.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RemoveOldGames()
        {
            try
            {
                GameEngine.GameEngine.RemoveOldGameBoards();

                Console.WriteLine("Removed old gameboards!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
