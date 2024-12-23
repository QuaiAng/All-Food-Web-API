using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{

    //Hàm tạo salt
    //Salt là 1 chuỗi ngẫu nhiên được thêm vào trước mật khẩu của user để đảm bảo các mật khẩu khác nhau băm ra chuỗi khác nhau
    public static string GenerateSalt(int length = 32)
    {
        var salt = new byte[length];
        using (var rng = RandomNumberGenerator.Create())  // Sử dụng RandomNumberGenerator mới
        {
            rng.GetBytes(salt); // Tạo salt ngẫu nhiên
        }
        return Convert.ToBase64String(salt); // Chuyển salt ngẫu nhiên thành chuỗi base64
    }

    //Hàm băm mật khẩu
    public static string HashPassword(string password, string salt)
    {
       
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(salt)))
        {
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashBytes);
        }
    }

    public static bool VerifyPassword(string hashedPassword, string password, string salt)
    {
        var newHashedPassword = HashPassword(password, salt);
        return hashedPassword == newHashedPassword;
    }


}
