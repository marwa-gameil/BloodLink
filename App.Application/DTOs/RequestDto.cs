using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs
{
    public record RequestDto
    {
        public int Id { get; set; }
        public string? BloodBankName { get; set; }
        public decimal Quantity { get; set; }
        public string BloodType { get; set; }
        public string PatientName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
       // public DateTime EndAt { get; set; }
       // public bool IsCanceled { get; set; }
    }
}
