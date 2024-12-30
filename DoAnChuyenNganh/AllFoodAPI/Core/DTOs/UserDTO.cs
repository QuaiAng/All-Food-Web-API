using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class UserDTO
    {


        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;



        public UserDTO()
        {
            UserId = this.UserId;
            Username = this.Username;
            FullName = this.FullName;
            Email = this.Email;
            Phone = this.Phone;
            ImageUrl = this.ImageUrl;
        }


        /// <summary>
        /// Return a DTO object from Entity object
        /// </summary>
        /// <param name="user">Entity object</param>
        /// <returns></returns>
        public static UserDTO FromEntity(User? user)
        {

            return new UserDTO()
            {

                UserId = user.UserId,
                ImageUrl = user.ImageUrl.Trim(),
                Phone = user.Phone.Trim(),
                FullName = user.FullName.Trim(),
                Email = user.Email.Trim(),
                Username = user.Username,
            };
        }

    }
}
