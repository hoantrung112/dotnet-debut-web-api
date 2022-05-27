using System.ComponentModel.DataAnnotations;
using System;
using DebutWebAPI.Models.Constants;

namespace DebutWebAPI.Models
{
    public class CitizenDto
    {
        public CitizenDto(Citizen citizen)
        {
            CitizenId = citizen.CitizenId;
            Username = citizen.Username;
            PhoneNumber = citizen.PhoneNumber;
            IdCardNumber = citizen.IdCardNumber;
            Address = citizen.Address;
            DOB = citizen.DOB;
            Email = citizen.Email;
            FullName = citizen.FullName;
        }
        public CitizenDto()
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

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime DOB { get; set; }

        public string IdCardNumber { get; set; }
        public string Address { get; set; }
    }
}
