using System.Collections.Generic;
using System.Threading.Tasks;

namespace DebutWebAPI.Models
{
    public interface ICitizenRepository
    {
        Task<IEnumerable<Citizen>> Search(string username, Gender? gender);
        Task<IEnumerable<Citizen>> GetCitizens();
        Task<Citizen> GetCitizen(long citizenId);
        Task<Citizen> GetCitizenByUsername(string username);
        Task<Citizen> GetCitizenByEmail(string email);
        Task<Citizen> GetCitizenByPhoneNumber(string phoneNumber);
        Task<Citizen> AddCitizen(Citizen citizen);
        Task<Citizen> UpdateCitizen(Citizen citizen);
        Task<Citizen> DeleteCitizen(long citizenId);
    }
}
