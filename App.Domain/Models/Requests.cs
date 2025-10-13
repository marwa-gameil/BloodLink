using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class Requests
    {
        public int Id { get; set; }
        public Hospitals hospitalId { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }
        public string BloodType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
