using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DebutWebAPI.Models.Constants;
namespace DebutWebAPI.Models
{
    public class Device
    {
        [Key]
        public long DeviceId { get; set; }
        [Required]
        public DeviceType DeviceType { get; set; }
        public DeviceBrand DeviceBrand { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        public Room Room { get; set; }
    }
}
