using MeetingAPI.Data;
using MeetingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingAPI.Repository
{
    public class MeetingTypeRepository
    {
        private readonly MeetingManagementContext appDbContext;

        public MeetingTypeRepository(MeetingManagementContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<MeetingType> GetByID(int id)
        {
            return await appDbContext.MeetingTypes
             .FirstOrDefaultAsync(d => d.MeetingTypeId == id);
            //.FindAsync(userId);
        }

        public async Task<IEnumerable<MeetingType>> GetAll()
        {
            return await appDbContext.MeetingTypes.ToListAsync();
        }

        public async Task<MeetingType> Add(MeetingType u)
        {
            var result = await appDbContext.MeetingTypes.AddAsync(u);

            await appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<MeetingType> Update(int id, string n)
        {
            var existingMeetingType = await appDbContext.MeetingTypes
                .FirstOrDefaultAsync(d => d.MeetingTypeId == id);

            if (existingMeetingType == null) return null;

            existingMeetingType.MeetingTypeName = n;

            await appDbContext.SaveChangesAsync();

            return existingMeetingType;
        }

        public async Task<bool> Delete(int id)
        {
            var m = await appDbContext.MeetingTypes
                .FirstOrDefaultAsync(d => d.MeetingTypeId == id);

            if (m == null) return false;

            appDbContext.MeetingTypes.Remove(m);

            await appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
