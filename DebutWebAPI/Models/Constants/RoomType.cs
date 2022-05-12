using System.ComponentModel.DataAnnotations;

namespace DebutWebAPI.Models.Constants
{
    public enum RoomType
    {
        [Display(Name = "Living Room")]
        LivingRoom,
        Bedroom,
        Kitchen,
        [Display(Name = "Dining Room")]
        DiningRoom,
        Restroom,
        Garden,
        [Display(Name = "Reading Room")]
        ReadingRoom,
        Balcony,
        Garage,
        Attic,
        Basement

    }
}
