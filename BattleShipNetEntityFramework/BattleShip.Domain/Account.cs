using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.SharedLibraries;

namespace BattleShip.Domain
{
    public class Account
    {
        private string _password;

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Player> Players { get; set; }
        public AccountRank Rank { get; set; }

        public int RankId { get; set; }

        public string Password {
            get {
                return _password;
            }
            set {
                if(!String.IsNullOrEmpty(value))
                {
                    _password = SecurePasswordHasher.Hash(value);
                }
                else
                {
                    _password = null;
                }
            }
        }

        public List<GameBoard> GameBoards
        {
            get
            {
                List<GameBoard> gameboards = new List<GameBoard>();

                foreach (Player player in Players)
                {
                    if (player.GameBoard != null)
                    {
                        gameboards.Add(player.GameBoard);
                    }
                }

                return gameboards;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Account()
        {
            Players = new List<Player>();
        }

        /// <summary>
        /// Deep copy to new account object (without password) 
        /// </summary>
        /// <returns>Deep copy</returns>
        public Account DeepCopy()
        {
            Account newAccount = (Account)this.MemberwiseClone();
            newAccount.Password = null;

            return newAccount;
        }

        /// <summary>
        /// Verify password against this Account object 
        /// </summary>
        /// <param name="passwordToCheck">Password string to check</param>
        /// <returns>Validate result</returns>
        public bool VerifyPassword(string passwordToCheck)
        {
            if (String.IsNullOrEmpty(_password))
            {
                throw new MissingFieldException("In this level, password field are not accessble!");
            }
                
            return SecurePasswordHasher.Verify(passwordToCheck, Password);
        }
    }
}