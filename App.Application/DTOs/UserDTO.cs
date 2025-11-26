using System;
using System.Collections.Generic;

namespace App.Application.DTOs
{
    public class UserDTO 
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

    }
}
