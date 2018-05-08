namespace BattleShip.Domain
{
    public class PlayerHit
    {
        public Position Position { get; set; }
        public Player Player { get; set; }

        public int PositionId { get; set; }
        public int PlayerId { get; set; }

        /// <summary>
        /// Deep copy PlayerHit object
        /// </summary>
        /// <returns>Deep copy</returns>
        public PlayerHit DeepCopy()
        {
            PlayerHit newPlayerHit = (PlayerHit)this.MemberwiseClone();

            if (newPlayerHit.Player != null && newPlayerHit.Player.Account != null)
            {
                newPlayerHit.Player.Account.Password = null;
            }

            return newPlayerHit;
        }
    }
}
