using MeetingAPI.Data;
using MeetingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingAPI.Repository
{
    public class MeetingRepository
    {
        private readonly MeetingManagementContext appDbContext;

        public MeetingRepository(MeetingManagementContext appContext)
        {
            appDbContext = appContext;
        }

        public async Task<IEnumerable<Meeting>> GetMeetings()
        {
            IEnumerable<Meeting> meetings = await appDbContext.Meetings.ToListAsync();
            return meetings;    
        }

        public async Task<Meeting> GetMeeting(int id)
        {
            return await appDbContext.Meetings
             .FirstOrDefaultAsync(d => d.MeetingId == id);
        }

        public async Task<Meeting> AddMeeting(Meeting m)
        {
            var result = await appDbContext.Meetings.AddAsync(m);

            await appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Meeting> UpdateMeeting(Meeting u)
        {
            var existingMeeting = await appDbContext.Meetings
                .FirstOrDefaultAsync(d => d.MeetingId == u.MeetingId);

            if (existingMeeting == null) return null;

            existingMeeting.MeetingTypeId = u.MeetingTypeId;
            existingMeeting.Title = u.Title;
            existingMeeting.Description = u.Description;
            existingMeeting.MeetingDate = u.MeetingDate;
            existingMeeting.StartTime = u.StartTime;
            existingMeeting.EndTime = u.EndTime;
            existingMeeting.Location = u.Location;

            await appDbContext.SaveChangesAsync();

            return existingMeeting;
        }

        public async Task<bool> UpdateMeetingStatus(int id, string val)
        {
            var existingMeeting = await appDbContext.Meetings
                .FindAsync(id);

            if (existingMeeting == null) return false;

            existingMeeting.Status = val;

            await appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteMeeting(int id)
        {
            var m = await appDbContext.Meetings
                .FindAsync(id);

            if(m == null) return false;

            appDbContext.Meetings.Remove(m);

            await appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
