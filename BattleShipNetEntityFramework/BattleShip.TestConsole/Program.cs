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
            //RemoveOldGames();
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
                Task<Account> account = GameEngine.GameEngine.Login("WrickedGamer", "Test123");
                Console.WriteLine("Account " + account.Result.UserName + " now logged in!");
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
                Task<Account> account = GameEngine.GameEngine.GetAccount(1);

                if (account.Result != null)
                {
                    Console.WriteLine("Got account with username: " + account.Result.UserName);
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
                Task<string> gameKey = GameEngine.GameEngine.NewGame(1, false);
                Console.WriteLine("New game added with gamekey: " + gameKey.Result);
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
                GameEngine.GameEngine.JoinGame("e86bf8", 2);
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
                Task<bool> keyExist = GameEngine.GameEngine.GameKeyExist("a11ab8");

                if (keyExist.Result)
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
                Task<GameBoard> game = GameEngine.GameEngine.GetGame("a11ab8");
                if (game.Result != null)
                {
                    Console.WriteLine(String.Format("Game {0} started by {1}", game.Result.Key, game.Result.Players[0].Account.UserName));
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
                Task<List<GameBoard>> games = GameEngine.GameEngine.GetOpenGames();

                Console.WriteLine("Existing open games:");

                foreach (GameBoard game in games.Result)
                {
                    Console.WriteLine("\t" + game.Key + " started by " + game.Players[0].Account.UserName);
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
                Task<List<GameBoard>> games = GameEngine.GameEngine.GetAccountGames(1);

                Console.WriteLine("WrickedGamer's games:");

                foreach (GameBoard game in games.Result)
                {
                    string activeText = (game.Active) ? "Active" : (game.Ended) ? "Game ended" : "Waiting for player";

                    Console.WriteLine(String.Format("\t{0} - {1}", game.Key, activeText));
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
                bool result = GameEngine.GameEngine.Shoot(14, 15, "a11ab8", 5, 5);

                Console.WriteLine("Shoot at position 5,5 on player 1 - Result:" + result.ToString());

                //bool result = GameEngine.GameEngine.Shoot(15, "a11ab8", 1, 1);

                //Console.WriteLine("Shoot at position 1,1 on player 2 - Result:" + result.ToString());

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
