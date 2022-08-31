using Internship_2022.Domain.Entities;
using Internship_2022.Infrastructure.Contexts;
using Internship_2022.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Internship_2022.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EFContext context;

        public UserRepository(EFContext context)
        {
            this.context = context;
        }

        public async Task DeleteUserById(Guid id)
        {
            var userToRemove = await context.Users.FirstAsync(entity => entity.Id == id);
            context.Users.Remove(userToRemove);
            await context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var restul = await context.Users.FirstOrDefaultAsync(entity => entity.Email == email);
            return restul;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await context.Users.Where(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddUser(User user)
        {
           context.Add(user);
           await context.SaveChangesAsync();
        }

        public async Task UpdateUserById(User user)
        {
            context.Update(user);
            await context.SaveChangesAsync();
        }


    }
}
