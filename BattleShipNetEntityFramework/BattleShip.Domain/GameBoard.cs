using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShip.Domain
{
    public class GameBoard
    {
        public string Key { get; set; }
        public bool Private { get; set; }
        public int? TurnPlayerId { get; set; }
        public DateTime LastUpdate { get; set; }

        public List<Player> Players { get; set; }
        public Player TurnPlayer { get; set; }

        /// <summary>
        /// Properties to check if game is active (not full or ended) - get
        /// </summary>
        public bool Active
        {
            get
            {
                return (Full && !Ended);
            }
        }

        /// <summary>
        /// Properties to return if game is full (two players) - get
        /// </summary>
        public bool Full
        {
            get
            {
                return (Players.Count >= 2);
            }
        }

        /// <summary>
        /// Properties to return if game is ended (one player's all boats are sink) - get
        /// </summary>
        public bool Ended
        {
            get
            {
                return Players.Any(p => p.Lost);
            }
        }

        /// <summary>
        /// Properties for check if both player have seen end screen - get
        /// </summary>
        public bool BothPlayerHasSeenEndScreen
        {
            get
            {
                return (Full && Players[0].HaveSeenEndScreen && Players[1].HaveSeenEndScreen);
            }
        }

        public GameBoard()
        {
            Players = new List<Player>();
            LastUpdate = DateTime.Now;
        }

        /// <summary>
        /// Deep copy Game object
        /// </summary>
        /// <returns>Deep copy</returns>
        public GameBoard DeepCopy()
        {
            GameBoard newGame = (GameBoard)this.MemberwiseClone();

            if (newGame.Players.Any() && newGame.Players[0].Account != null)
            {
                newGame.Players.ForEach(p => p.Account.Password = null);
            }

            return newGame;
        }
    }
}