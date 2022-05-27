using DebutWebAPI.Models.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using DebutWebAPI.Models.Dto;

namespace DebutWebAPI.Models.Repositories
{

    public class SmartHomeRepository : ISmartHomeRepository
    {
        private readonly AppContext appContext;

        public SmartHomeRepository(AppContext appContext)
        {
            this.appContext = appContext;
        }
        public async Task<SmartHome> AddSmartHome(SmartHome smartHome)
        {
            var existedSmartHome = await GetSmartHome(smartHome.SmartHomeId);
            if (existedSmartHome != null)
            {
                throw new Exception("Smart Home already existed!");
            }
            var newSmartHome = await appContext.SmartHomes.AddAsync(smartHome);
            await appContext.SaveChangesAsync();
            return newSmartHome.Entity;
        }

        public async Task<SmartHome> DeleteSmartHome(long smartHomeId)
        {
            var targetSmartHome = await appContext.SmartHomes
                .FirstOrDefaultAsync(e => e.SmartHomeId == smartHomeId);
            if (targetSmartHome != null)
            {
                appContext.SmartHomes.Remove(targetSmartHome);
                await appContext.SaveChangesAsync();
                return targetSmartHome;
            }

            return null;
        }

        public async Task<SmartHome> GetSmartHome(long smartHomeId)
        {
            return await appContext.SmartHomes
               //.Include(e => e.Department)
               .FirstOrDefaultAsync(e => e.SmartHomeId == smartHomeId);
        }

        public async Task<SmartHome> GetSmartHomeByType(SmartHomeType smartHomeType)
        {
            return await appContext.SmartHomes
                .FirstOrDefaultAsync(e => e.SmartHomeType == smartHomeType);
        }

        public async Task<IEnumerable<SmartHome>> GetSmartHomes()
        {
            return await appContext.SmartHomes.ToListAsync();
        }

        public async Task<IEnumerable<SmartHome>> GetSmartHomesByDisctrict(string district)
        {
            //var smartHomes = (from home in appContext.SmartHomes select home);
            //return await smartHomes.Where(h => h.District);
            return await appContext.SmartHomes
                .Where(e => e.District.ToString() == district).ToListAsync();
        }

        public async Task<IEnumerable<SmartHome>> Search(SmartHomeType smartHomeType, string address)
        {
            IQueryable<SmartHome> query = appContext.SmartHomes;

            if (!string.IsNullOrEmpty(smartHomeType.ToString()))
            {
                query = query.Where(e => e.SmartHomeType.ToString().Contains(smartHomeType.ToString()));
            }

            if (address != null)
            {
                query = query.Where(e => e.Address == address);
            }

            return await query.ToListAsync();
        }

        public async Task<SmartHome> UpdateSmartHome(SmartHome smartHome)
        {
            var targetSmartHome = await appContext.SmartHomes
               .FirstOrDefaultAsync(e => e.SmartHomeId == smartHome.SmartHomeId);

            if (targetSmartHome != null)
            {
                targetSmartHome.SmartHomeType = smartHome.SmartHomeType;
                targetSmartHome.Address = smartHome.Address;
                targetSmartHome.District = smartHome.District;
                targetSmartHome.Rooms = smartHome.Rooms;
                targetSmartHome.SmartHomeOwners = smartHome.SmartHomeOwners;
                targetSmartHome.SmartHomeCitizens = smartHome.SmartHomeCitizens;

                await appContext.SaveChangesAsync();

                return targetSmartHome;
            }

            return null;
        }
        public List<SmartHomeDto> ConvertToSmartHomeDTO(List<SmartHome> smartHomes)
        {
            List<SmartHomeDto> result = new List<SmartHomeDto>();
            foreach (var sh in smartHomes)
            {
                result.Add(new SmartHomeDto(sh));
            }
            return result;
        }
    }
}
