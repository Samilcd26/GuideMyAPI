using GuideAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace GuideAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        DataContext _contex;
        public AuthRepository(DataContext context)
        {
            _contex = context;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await _contex.Users.FirstOrDefaultAsync(x=>x.UserName == userName);

            if (user==null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] userpasswordHash, byte[] userpasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userpasswordSalt))
            {
               var computeHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i=0; i<computeHash.Length; i++)
                {
                    if(computeHash[i] != userpasswordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _contex.Users.AddAsync(user);
            await _contex.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            if(await _contex.Users.AnyAsync(x=>x.UserName == userName))
            {
                return true;
            }
            return false;
        }
    }
}
