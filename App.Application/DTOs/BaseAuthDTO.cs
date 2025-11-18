using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.DTOs
{
    public record BaseAuthDTO
    {
        [Required, EmailAddress]
        public required string Email { get; init; }

        [Required]
        public required string Password { get; init; }
    }
}
