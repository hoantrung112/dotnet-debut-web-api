namespace DebutWebAPI.Models
{
    public class SmartHomeOwner
    {
        public long SmarHomeId { get; set; }
        public SmartHome SmartHome { get; set; }
        public long OwnerId { get; set; }
        public Citizen Owner { get; set; }
    }
}
