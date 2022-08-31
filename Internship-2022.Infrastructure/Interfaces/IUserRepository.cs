using Internship_2022.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task UpdateUserById (User user);
        Task<User?> GetUserById(Guid id);
        Task<List<User>> GetAllUsers();
        Task DeleteUserById(Guid id);
        Task<User?> GetUserByEmail(string email);
    }
}
