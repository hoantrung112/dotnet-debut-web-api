using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using DebutWebAPI.Models.Constants;
namespace DebutWebAPI.Models
{
    public class SmartHome
    {
        [Key]
        public long SmartHomeId { get; set; }

        public SmartHomeType SmartHomeType { get; set; }

        public District District { get; set; }
        public string Address { get; set; }
        public IList<SmartHomeCitizen> SmartHomeCitizens { get; set; }
        public IList<SmartHomeOwner> SmartHomeOwners { get; set; }

        public IList<Room> Rooms { get; set; }

    }
}
