using System.ComponentModel.DataAnnotations;
using System;
using DebutWebAPI.Models.Constants;
using System.Collections.Generic;

namespace DebutWebAPI.Models
{
    public class Citizen
    {
        public Citizen(CitizenDto citizenDto)
        {            
            CitizenId = citizenDto.CitizenId;
            Username = citizenDto.Username;
            PhoneNumber = citizenDto.PhoneNumber;
            IdCardNumber = citizenDto.IdCardNumber;
            Address = citizenDto.Address;
            DOB = citizenDto.DOB;
            Email = citizenDto.Email;
            FullName = citizenDto.FullName;
        }
        public Citizen()
        {
            
        }
        [Key]
        public long CitizenId { get; set; }

        [Required(ErrorMessage = "Username must be provided")]
        [MinLength(6, ErrorMessage = "Username must be at least 6-character long!")]
        [StringLength(15)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Full name must be provided")]
        [MinLength(6, ErrorMessage = "Fullname must be at least 6-character long!")]
        [StringLength(30)]
        public string FullName { get; set; }

        public Gender Gender { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime DOB { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string IdCardNumber { get; set; }
        public string AvatarURL { get; set; }
        public string Address { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }

        public IList<SmartHomeCitizen> SmartHomeCitizens { get; set; }
        public IList<SmartHomeOwner> SmartHomeOwners { get; set; }
    }
}
