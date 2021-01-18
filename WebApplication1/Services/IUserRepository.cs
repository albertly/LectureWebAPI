using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUser(Guid userId);

        void AddUser(User user);

        void UpdateUser(User user);

        void DeleteUser(Guid userId);

        bool UserExists(Guid userId);

        void AddClaim(Guid userId, Claim claim);

        Claim GetClaim(Guid userId, Guid claimId);

        bool Save();
    }
}
