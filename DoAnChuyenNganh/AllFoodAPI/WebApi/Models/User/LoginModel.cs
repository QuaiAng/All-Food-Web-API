﻿namespace AllFoodAPI.WebApi.Models.User
{

    public class LoginModel
    {
        public required string username { get; set; }
        public required string password { get; set; }
    }
}