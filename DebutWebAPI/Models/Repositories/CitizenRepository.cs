using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DebutWebAPI.Models
{
    public class CitizenRepository : ICitizenRepository
    {
        private readonly AppContext appContext;


        public CitizenRepository(AppContext appContext)
        {
            this.appContext = appContext;
        }

        public async Task<Citizen> AddCitizen(Citizen citizen)
        {
            var citizenWithEmail = await GetCitizenByEmail(citizen.Email);
            var citizenWithUsername = await GetCitizenByEmail(citizen.Username);
            if (citizenWithEmail != null || citizenWithUsername != null)
            {
                throw new Exception("Email or username has already been used!");
            }
            var newCitizen = await appContext.Citizens.AddAsync(citizen);
            await appContext.SaveChangesAsync();
            return newCitizen.Entity;
        }

        public async Task<Citizen> DeleteCitizen(long citizenId)
        {
            var targetCitizen = await appContext.Citizens
                .FirstOrDefaultAsync(e => e.CitizenId == citizenId);
            if (targetCitizen != null)
            {
                appContext.Citizens.Remove(targetCitizen);
                await appContext.SaveChangesAsync();
                return targetCitizen;
            }

            return null;
        }

        public async Task<Citizen> GetCitizen(long citizenId)
        {
            return await appContext.Citizens
                //.Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.CitizenId == citizenId);
        }

        public async Task<Citizen> GetCitizenByUsername(string username)
        {
            return await appContext.Citizens
                .FirstOrDefaultAsync(e => e.Username == username);
        }
        public async Task<Citizen> GetCitizenByPhoneNumber(string phoneNumber)
        {
            return await appContext.Citizens
                .FirstOrDefaultAsync(e => e.PhoneNumber == phoneNumber);
        }
        public async Task<Citizen> GetCitizenByEmail(string email)
        {
            return await appContext.Citizens
                .FirstOrDefaultAsync(e => e.Email == email);
        }


        public async Task<IEnumerable<Citizen>> GetCitizens()
        {
            return await appContext.Citizens.ToListAsync();
        }

        public async Task<Citizen> UpdateCitizen(Citizen citizen)
        {
            var targetCitizen = await appContext.Citizens
                .FirstOrDefaultAsync(e => e.CitizenId == citizen.CitizenId);

            // validation
            //var oldCitizen = await GetCitizenByEmail(citizen.Email);
            //if (oldCitizen != null)
            //{
            //    throw new Exception("This email has already been used!");
            //}


            if (targetCitizen != null)
            {
                targetCitizen.Username = citizen.Username;
                targetCitizen.FullName = citizen.FullName;
                targetCitizen.Email = citizen.Email;
                targetCitizen.DOB = citizen.DOB;
                targetCitizen.Gender = citizen.Gender;
                targetCitizen.Email = citizen.Email;

                await appContext.SaveChangesAsync();

                return targetCitizen;
            }

            return null;
        }

        public async Task<IEnumerable<Citizen>> Search(string username, Gender? gender)
        {
            IQueryable<Citizen> query = appContext.Citizens;

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(e => e.Username.Contains(username));
            }

            if (gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }
    }
}

