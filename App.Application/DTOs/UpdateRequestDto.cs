
namespace App.Application.DTOs
{
  
    public class UpdateRequestDto
    {
        public decimal Quantity { get; set; }
        public string Status { get; set; } 
        public DateTime EndAt { get; set; }
        public string BloodType { get; set; }
    }
}
