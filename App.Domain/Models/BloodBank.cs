using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class BloodBank
    {
        public float Latitude { get; set; } //coordX 
        public float Longitude { get; set; } //coordY

        public string LicenseNumber { get; set; }

        public TimeOnly StartWorkingHours { get; set; }
        public TimeOnly EndWorkingHours { get; set; }
        public Guid UserId { get; set; } //fk to user
        public User User { get; set; }
        public ICollection<BloodRequest> Requests { get; set; } = new List<BloodRequest>();


    }
}
