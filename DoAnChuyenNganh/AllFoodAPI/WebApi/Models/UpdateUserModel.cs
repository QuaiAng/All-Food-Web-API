namespace AllFoodAPI.WebApi.Models
{
    public class UpdateUserModel
    {
        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public bool? Status { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
