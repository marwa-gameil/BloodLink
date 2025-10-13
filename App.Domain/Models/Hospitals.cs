using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class Hospitals
    {
        public int HospitalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public float Latitude { get; set; } //coordX 
        public float Longitude { get; set; } //coordY

        public TimeOnly StartWorkingHours { get; set; }
        public TimeOnly EndWorkingHours { get; set; }
       
        public DateTime CreatedAt { get; set; }
        public User CreatedBy { get; set; }
    }
}
