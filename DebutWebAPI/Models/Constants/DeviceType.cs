using System.ComponentModel.DataAnnotations;

namespace DebutWebAPI.Models.Constants
{
    public enum DeviceType
    {
        [Display(Name = "Washing Machine")]
        WashingMachine,
        [Display(Name = "Ceiling Fan")]
        CeilingFan,
        [Display(Name = "Standing Fan")]
        StandingFan,
        Curtain,
        [Display(Name = "Dish Washer")]
        DishWasher,
        [Display(Name = "Robot Vacuum")]
        RobotVacuum,
        TV,
        Speaker,
        Refrigerator,
        [Display(Name = "Air Conditioner")]
        AirConditioner,
        [Display(Name = "Air Cleaner")]
        AirCleaner,
        Wifi,
        Doorbell,
        Camera

    }
}
