using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DbContexts;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public class UserRepository : IUserRepository, IDisposable
    {

        private readonly SQLContext _context;

        public UserRepository(SQLContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public void AddClaim(Guid userId, Claim claim)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            claim.UserId = userId;
            _context.Claims.Add(claim);
            //_context.Claims.FromSqlRaw
            //_context.Database.GetDbConnection
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Id = Guid.NewGuid();

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

        public Claim GetClaim(Guid userId, Guid claimId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (claimId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(claimId));
            }

            return _context.Claims
              .Where(c => c.UserId == userId && c.Id == claimId).FirstOrDefault();
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync <User>(a => a.Id == userId);

        }

        public async Task<IEnumerable<User>> GetUsers()
        {
           return await _context.Users.ToListAsync<User>();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _context.Users.Any(u => u.Id == userId);
        }
    }
}
