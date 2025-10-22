using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public enum BloodRequestStatus
    {
        Pending,
        Approved,
        Completed,
        Rejected
        
    }

    public class BloodRequest
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        public int BloodBankId { get; set; }
        public BloodBank BloodBank { get; set; }

        public decimal Quantity { get; set; }
        public BloodRequestStatus Status { get; set; }
        public string BloodType { get; set; }

        //public bool IsCanceled { get; set; }
        public string PatientName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndAt { get; set; }

        public bool IsCanceled = false;
    }
}
