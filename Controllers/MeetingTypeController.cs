using MeetingAPI.Models;
using MeetingAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingTypeController : ControllerBase
    {
        private readonly MeetingTypeRepository _meetingTypeRepository;

        public MeetingTypeController(MeetingTypeRepository repository)
        {
            this._meetingTypeRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetMeetingTypes()
        {
            try
            {
                return Ok(await _meetingTypeRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MeetingType>> GetMeetingType(int id)
        {
            try
            {
                var result = await _meetingTypeRepository.GetByID(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<MeetingType>> CreateMeetingType(MeetingType u)
        {
            try
            {
                if (u == null)
                    return BadRequest("MeetingType cannot be null");

                var createdMeetingType = await _meetingTypeRepository.Add(u);
                return CreatedAtAction(nameof(GetMeetingType), new { id = createdMeetingType.MeetingTypeId }, createdMeetingType);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<MeetingType>> UpdateMeetingType(int id, string n)
        {
            try
            {

                var updatedMeetingType = await _meetingTypeRepository.Update(id, n);
                if (updatedMeetingType == null)
                    return NotFound($"MeetingType with Id = {id} not found");

                return updatedMeetingType;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMeetingType(int id)
        {
            try
            {
                var success = await _meetingTypeRepository.Delete(id);
                if (!success)
                    return NotFound($"MeetingType with Id = {id} not found");

                return Ok("Meeting type deleted successfully!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting Meeting type");
            }
        }
    }
}
