using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Web.Models
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
        public Guid HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public Guid BloodBankId { get; set; }
        public BloodBank BloodBank { get; set; }
        public decimal Quantity { get; set; }
        public BloodRequestStatus Status { get; set; }
        public BloodType BloodType { get; set; }
        public string PatientName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndAt { get; set; }

        public bool IsCanceled = false;
    }
}
