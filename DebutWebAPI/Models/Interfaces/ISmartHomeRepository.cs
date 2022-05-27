using System.Collections.Generic;
using System.Threading.Tasks;
using DebutWebAPI.Models.Constants;
using DebutWebAPI.Models.Dto;

namespace DebutWebAPI.Models
{
    public interface ISmartHomeRepository
    {
        Task<IEnumerable<SmartHome>> Search(SmartHomeType smartHomeType, string address);
        Task<IEnumerable<SmartHome>> GetSmartHomes();
        Task<IEnumerable<SmartHome>> GetSmartHomesByDisctrict(string district);
        Task<SmartHome> GetSmartHome(long smartHomeId);
        Task<SmartHome> GetSmartHomeByType(SmartHomeType smartHomeType);
        Task<SmartHome> AddSmartHome(SmartHome smartHome);
        Task<SmartHome> UpdateSmartHome(SmartHome smartHome);
        Task<SmartHome> DeleteSmartHome(long smartHomeId);
        List<SmartHomeDto> ConvertToSmartHomeDTO(List<SmartHome> smartHomes);
    }
}
