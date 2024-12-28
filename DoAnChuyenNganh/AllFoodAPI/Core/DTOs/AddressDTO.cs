using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class AddressDTO
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; } = null!;

      
        public AddressDTO() { }

        

        public static Address ToEntity(AddressDTO addressDTO)
        {
            return new Address
            {
                AddressId = addressDTO.AddressId,
                UserId = addressDTO.UserId,
                Address1 = addressDTO.Address.Trim()
            };
        }

        public static AddressDTO FromEntity(Address address)
        {
            return new AddressDTO
            {
                AddressId = address.AddressId,
                UserId = address.UserId,
                Address = address.Address1.Trim()
            };
        }
    }
}
