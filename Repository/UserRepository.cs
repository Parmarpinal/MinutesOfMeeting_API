using MeetingAPI.Data;
using MeetingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingAPI.Repository
{
    public class UserRepository
    {
        private readonly MeetingManagementContext appDbContext;

        public UserRepository(MeetingManagementContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<User> GetUser(int userId)
        {
            return await appDbContext.Users
             .FirstOrDefaultAsync(d => d.UserId == userId);
            //.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await appDbContext.Users.ToListAsync();
        }

        public async Task<User> AddUser(User u)
        {
            var result = await appDbContext.Users.AddAsync(u);

            await appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<User> UpdateUser(User u)
        {
            var existingUser = await appDbContext.Users
                .FirstOrDefaultAsync(d => d.UserId == u.UserId);

            if (existingUser == null) return null;

            existingUser.UserName = u.UserName;
            existingUser.Email = u.Email;
            existingUser.Password = u.Password;
            existingUser.MobileNo = u.MobileNo;
            existingUser.Gender = u.Gender;
            existingUser.Age = u.Age;
            existingUser.City = u.City;
            existingUser.ImagePath = u.ImagePath;

            await appDbContext.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var userToDelete = await appDbContext.Users
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (userToDelete == null) return false;

            appDbContext.Users.Remove(userToDelete);

            await appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
