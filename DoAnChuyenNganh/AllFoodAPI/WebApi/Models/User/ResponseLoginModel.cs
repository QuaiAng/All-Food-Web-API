namespace AllFoodAPI.WebApi.Models.User
{
    public class ResponseLoginModel
    {
        public int UserId { get; set; } = 0;

        public string Token { get; set; } = null!;

    }
}
