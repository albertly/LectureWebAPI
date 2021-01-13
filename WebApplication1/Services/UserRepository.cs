using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DbContexts;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserRepository : IUserRepository, IDisposable
    {

        private readonly SQLContext _context;

        public UserRepository(SQLContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            OptionsWrapper<PasswordHasherOptions> options = new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions
            {
                CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
            });

            PasswordHasher<User> hasher = new PasswordHasher<User>(options);

            user.Password = hasher.HashPassword(user, user.Password);
            
            _context.Users.Add(user);

          
        }

        public void DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public User GetUser(Guid userId)
        {
           return _context.Users.FirstOrDefault<User>(a => a.Id == userId);
            
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
