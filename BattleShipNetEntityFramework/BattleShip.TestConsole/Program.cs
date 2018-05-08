using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.Domain;
using BattleShip.Engines;

namespace BattleShip.TestConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            //AddAccount();
            Login();
            //GetAccount();
            //SendRecoveryAccount();
            //RecoveryAccount();
            //SaveAccountRecovery();
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
                GameEngine.NewAccount("WrickedGamer2", "Test123", "wrickedgamer2@example.com");
                //GameEngine.NewAccount("Chibi", "Test123", "chibi@example.com");

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
                Account account = GameEngine.Login("Chibi", "Test1234");
                Console.WriteLine("Account " + account.UserName + " now logged in!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SendRecoveryAccount()
        {
            try
            {
                GameEngine.SendAccountRecovery("Chibi", "http://example.com/accountrecovery");
                Console.WriteLine("Email for recovery account is send!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RecoveryAccount()
        {
            try
            {
                AccountRecovery recovery = GameEngine.AccountRecovery(9, "6777e4534f");

                Console.WriteLine("You are about to change password for account with username: " + recovery.Account.UserName + ", recovery is valid until " + recovery.ExpireDate.ToString("yyyy-MM-dd hh:mm"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SaveAccountRecovery()
        {
            try
            {
                GameEngine.SaveAccountRecovery(2, "9ea1581adf", "Test1234");
                Console.WriteLine("Password is change!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetAccount()
        {
            try
            {
                Account account = GameEngine.GetAccount(1);

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
                string gameKey = GameEngine.NewGame(1, false);
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
                GameEngine.JoinGame("e86bf8", 2);
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
                bool keyExist = GameEngine.GameKeyExist("a11ab8");

                if (keyExist)
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
                GameBoard game = GameEngine.GetGame("a53d7b");
                if (game != null)
                {
                    Console.WriteLine(String.Format("Game {0} started by {1}", game.Key, game.Players[0].Account.UserName));
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
                List<GameBoard> games = GameEngine.GetOpenGames();

                Console.WriteLine("Existing open games:");

                foreach (GameBoard game in games)
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
                List<GameBoard> games = GameEngine.GetAccountGames(1);

                Console.WriteLine("WrickedGamer's games:");

                foreach (GameBoard game in games)
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
                bool result = GameEngine.Shoot(14, 15, "a11ab8", 5, 5);

                Console.WriteLine("Shoot at position 5,5 on player 1 - Result:" + result.ToString());

                //bool result = GameEngine.Shoot(15, "a11ab8", 1, 1);

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
                GameEngine.RemoveOldGameBoards();

                Console.WriteLine("Removed old gameboards!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
