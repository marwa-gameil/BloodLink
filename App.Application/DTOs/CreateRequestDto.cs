using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs
{
    public class CreateRequestDto
    {
        public Guid BloodBankId { get; set; }
        public decimal Quantity { get; set; }
        public string BloodType { get; set; }
        public string PatientName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime EndAt { get; set; }
    }
}
