namespace BattleShip.Domain
{
    public class BoatHit
    {
        public Position Position { get; set; }
        public Boat Boat { get; set; }

        public int PositionId { get; set; }
        public int BoatId { get; set; }
    }
}