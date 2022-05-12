using DebutWebAPI.Models.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace DebutWebAPI.Models
{
    public class Room
    {
        [Key]
        public long RoomId { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        public Citizen OwnerId { get; set; }
        public IList<RoomDevice> RoomDevices { get; set; }

        public SmartHome SmartHome { get; set; }
    }
}
