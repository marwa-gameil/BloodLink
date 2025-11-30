
namespace App.Web.Models
{
    public enum BloodType
    {
        A_Positive,
        A_Negative,
        B_Positive,
        B_Negative,
        AB_Positive,
        AB_Negative,
        O_Positive,
        O_Negative
    }
    public class Stock
    {
        public int Id { get; set; }

        public Guid BloodBankId { get; set; }      //fk
        public BloodBank BloodBank { get; set; }
        public decimal Quantity { get; set; }
        public BloodType BloodType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
