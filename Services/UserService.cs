using BlazorApp1.Data;
using BlazorApp1.Models;
using BlazorApp1.Utils;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp1.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterAsync(string username, string password, Role role = Role.User)
        {
            try
            {
                PasswordHelper.CreatePasswordHash(password, out var hash, out var salt);

                var user = new User
                {
                    Username = username,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Role = role
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user is null) return null;

            if (PasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                Console.WriteLine("User : " + user + "-------------------------------");
                return user;
            }
            else { return null; }

        }

        public async Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return false;

            var isPasswordTrue = PasswordHelper.VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt);

            if (!isPasswordTrue) return false;

            PasswordHelper.CreatePasswordHash(newPassword, out var hash, out var salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            await _context.SaveChangesAsync();
            return true;
        }


    }
}
