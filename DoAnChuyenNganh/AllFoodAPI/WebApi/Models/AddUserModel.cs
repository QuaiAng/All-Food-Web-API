﻿namespace AllFoodAPI.WebApi.Models
{
    public class AddUserModel
    {
        
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

    }
}
