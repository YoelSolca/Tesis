using Backend.Application.Interfaces;

namespace Backend.Infrastructure.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        bool IPasswordHasher.Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
