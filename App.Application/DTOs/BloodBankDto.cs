using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs
{
    public class BloodBankDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Governorate { get; set; }
    }

    public class CreateBloodBankDto
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]

        public string Governorate { get; set; }
        [Required]

        public string PhoneNumber { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public float Latitude { get; set; } //coordX 
        [Required]

        public float Longitude { get; set; } //coordY
        [Required]

        public string LicenseNumber { get; set; }

        [Required]
        public required string Password { get; set; }

    }
    public class UpdateBloodBankDto
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
      

    }
}
