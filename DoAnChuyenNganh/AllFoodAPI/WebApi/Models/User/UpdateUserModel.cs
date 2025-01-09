namespace AllFoodAPI.WebApi.Models.User
{
    public class UpdateUserModel
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
