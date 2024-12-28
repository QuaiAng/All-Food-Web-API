using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class UserDTO
    {
        

        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public bool? Status { get; set; }

        public string ImageUrl { get; set; } = null!;
        public string Salt { get; set; } = null!;



        public UserDTO()
        {
            UserId = this.UserId;
            Username = this.Username;
            Password = this.Password;
            FullName = this.FullName;
            Email = this.Email;
            Phone = this.Phone;
            Status = this.Status;
            ImageUrl = this.ImageUrl;
            Salt = this.Salt;
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
                Status = user.Status,
                ImageUrl = user.ImageUrl.Trim(),
                Phone = user.Phone.Trim(),
                Password = user.Password,
                FullName = user.FullName.Trim(),
                Email = user.Email.Trim(),
                Username = user.Username,
                Salt = user.Salt,
            };
        }


        /// <summary>
        /// Return a Entity object from DTO object
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public static User ToEntity(UserDTO? userDTO)
        {
            return new User()
            {

                UserId = userDTO.UserId,
                Status = userDTO.Status,
                ImageUrl = userDTO.ImageUrl.Trim(),
                Phone = userDTO.Phone.Trim(),
                Password = userDTO.Password,
                FullName = userDTO.FullName.Trim(),
                Email = userDTO.Email.Trim(),
                Username= userDTO.Username,
                Salt= userDTO.Salt,

            }    
            ;
        }
    }
}
