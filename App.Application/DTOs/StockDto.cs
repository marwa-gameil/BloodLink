using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs
{
    public class StockDto
    {
        public int Id { get; set; }
        public string BloodBankName { get; set; }
        public decimal Quantity { get; set; }
        public string BloodType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateStockDto
    {
        public int BloodBankId { get; set; }
        public decimal Quantity { get; set; }
        public string BloodType { get; set; } 
    }
    public class UpdateStockDto
    {
        public decimal Quantity { get; set; }
        public string BloodType { get; set; }
    }
}
