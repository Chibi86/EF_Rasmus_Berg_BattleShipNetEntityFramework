namespace BattleShip.Domain
{
    public class PlayerHit
    {
        public Position Position { get; set; }
        public Player Player { get; set; }

        public int PositionId { get; set; }
        public int PlayerId { get; set; }
    }
}
