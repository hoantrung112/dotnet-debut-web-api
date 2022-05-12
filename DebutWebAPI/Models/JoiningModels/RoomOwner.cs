namespace DebutWebAPI.Models
{
    public class RoomOwner
    {
        public long RoomId { get; set; }
        public Room Room { get; set; }
        public long OwnerId { get; set; }
        public Citizen Owner { get; set; }
    }
}
