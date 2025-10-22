using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
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

        public int BloodBankId { get; set; }     
        public BloodBank BloodBank { get; set; }
        public decimal Quantity { get; set; }
        public BloodType BloodType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
