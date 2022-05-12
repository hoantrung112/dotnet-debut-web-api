namespace DebutWebAPI.Models
{
    public class RoomDevice
    {
        public long RoomId { get; set; }
        public Room Room { get; set; }
        public long DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
