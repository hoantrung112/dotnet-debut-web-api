using System.ComponentModel.DataAnnotations;

namespace DebutWebAPI.Models.Constants
{
    public enum DeviceBrand
    {
        Samsung,
        LG,
        Sonos,
        Lutron,
        IRobot,
        Nest,
        Ecobee,
        Funiki,
        Panasonic,
        Daikin,
        [Display(Name = "Philips Hue")]
        PhilipsHue,
        [Display(Name = "Amazon Alexa")]
        AmazonAlexa

    }
}
