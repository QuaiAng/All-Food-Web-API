namespace AllFoodAPI.WebApi.Models.User
{
    public class ChangePasswordModel
        
    {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;

    }
}
