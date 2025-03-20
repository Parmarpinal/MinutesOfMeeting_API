using MeetingAPI.Models;
using MeetingAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace MeetingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private MeetingRepository _meetingRepository;
        public MeetingController(MeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMeetings()
        {
            try
            {
                var meetings = await _meetingRepository.GetMeetings();
                return Ok(meetings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data from the database");
            }           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetingByID(int id)
        {
            try
            {
                var meeting = await _meetingRepository.GetMeeting(id);
                if (meeting == null)
                {
                    return NotFound();
                }
                return Ok(meeting);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Meeting>> CreateMeeting(Meeting u)
        {
            try
            {
                if (u == null)
                    return BadRequest("Meeting cannot be null");

                var createdMeeting = await _meetingRepository.AddMeeting(u);
                return CreatedAtAction(nameof(GetMeetingByID), new { id = createdMeeting.MeetingId }, createdMeeting);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Meeting>> UpdateMeeting(int id, Meeting user)
        {
            try
            {
                if (id != user.MeetingId)
                    return BadRequest("Meeting ID mismatch");

                var updatedMeeting = await _meetingRepository.UpdateMeeting(user);
                if (updatedMeeting == null)
                    return NotFound($"Meeting with Id = {id} not found");

                return updatedMeeting;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpPut("/UpdateStatus/{id:int}")]
        public async Task<ActionResult<Meeting>> UpdateMeetingStatus(int id, string val)
        {
            try
            {
                if (id == null)
                    return NotFound($"Meeting with Id = {id} not found");

                var isUpdated = await _meetingRepository.UpdateMeetingStatus(id, val);
                if (!isUpdated)
                {
                    return StatusCode(500);
                }

                return Ok($"The status of MeetingId: {id} is => {val}");
              
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMeeting(int id)
        {
            try
            {
                var success = await _meetingRepository.DeleteMeeting(id);
                if (!success)
                    return NotFound($"Meeting with Id = {id} not found");

                return Ok("Meeting deleted successfully!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting meeting");
            }
        }
    }
}
