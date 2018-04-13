using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Domain
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Player> Players { private get; set; }
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

        public Account()
        {
            Players = new List<Player>();
        }
    }
}