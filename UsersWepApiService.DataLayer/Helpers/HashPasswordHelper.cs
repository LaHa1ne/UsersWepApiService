using System.Text;
using System.Security.Cryptography;


namespace UsersWepApiService.DataLayer.Helpers
{
    public class HashPasswordHelper
    {
        public static string GetHashPassword(string Password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(buffer: Encoding.UTF8.GetBytes(Password));
                return BitConverter.ToString(hashedBytes, 0, hashedBytes.Length).Replace("-", "").ToLower();
            }
        }
    }
}
