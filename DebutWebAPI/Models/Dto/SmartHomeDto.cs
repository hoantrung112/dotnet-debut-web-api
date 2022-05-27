using DebutWebAPI.Models.Constants;
using System.Collections.Generic;
namespace DebutWebAPI.Models.Dto
{
    public class SmartHomeDto
    {
        public SmartHomeDto(SmartHome smartHome)
        {
            SmartHomeId = smartHome.SmartHomeId;
            SmartHomeType = smartHome.SmartHomeType;
            Address = smartHome.Address;
            District = smartHome.District;
            //List<Citizen> citizens = new List<Citizen>(smartHome.SmartHomeCitizens);
            //foreach(var citizen in citizens)
            //{
            //    SmartHomeCitizens.Add(new CitizenDto(citizen));
            //}
        }
        public SmartHomeDto()
        {

        }
        public long SmartHomeId { get; set; }

        public SmartHomeType SmartHomeType { get; set; }

        public District District { get; set; }
        public string Address { get; set; }
        public IList<CitizenDto> SmartHomeCitizens { get; set; }
        public IList<CitizenDto> SmartHomeOwners { get; set; }

        //public IList<Room> Rooms { get; set; }
    }
}
