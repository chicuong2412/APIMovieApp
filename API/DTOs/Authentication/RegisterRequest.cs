﻿using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Authentication
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
