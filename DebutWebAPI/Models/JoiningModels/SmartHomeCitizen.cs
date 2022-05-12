namespace DebutWebAPI.Models
{
    public class SmartHomeCitizen
    {
        public long SmarHomeId { get; set; }
        public SmartHome SmartHome { get; set; }
        public long CitizenId { get; set; }
        public Citizen Citizen { get; set; }
    }
}
