using MauiWebApi.Entities;
using MauiWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MauiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventDatabase _db = new EventDatabase();

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<List<SpecialEvent>>> GetEvents()
        {
            var events = await _db.GetSpecialEventsAsync();

            return Ok(events);
        }


        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialEvent>> GetEvent(int id)
        {
            var specialEvent = await _db.GetSpecialEventAsync(id);

            if (specialEvent == null)
            {
                return NotFound($"ID: {id} olan etkinlik bulunamadı.");
            }

            return Ok(specialEvent);
        }


        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<SpecialEvent>> CreateEvent(
            [FromBody] SpecialEvent specialEvent)
        {
            if (specialEvent == null)
            {
                return BadRequest("Etkinlik bilgileri boş olamaz.");
            }

            await _db.AddSpecialEventAsync(specialEvent);

            return CreatedAtAction(
                nameof(GetEvent),
                new { id = specialEvent.Id },
                specialEvent);
        }


        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(
            int id,
            [FromBody] SpecialEvent specialEvent)
        {
            if (specialEvent == null)
            {
                return BadRequest("Etkinlik bilgileri boş olamaz.");
            }

            if (id != specialEvent.Id)
            {
                return BadRequest("URL'deki ID ile etkinliğin ID'si aynı olmalıdır.");
            }

            var existingEvent = await _db.GetSpecialEventAsync(id);

            if (existingEvent == null)
            {
                return NotFound($"ID: {id} olan etkinlik bulunamadı.");
            }

            await _db.UpdateSpecialEventAsync(specialEvent);

            return NoContent();
        }


        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var existingEvent = await _db.GetSpecialEventAsync(id);

            if (existingEvent == null)
            {
                return NotFound($"ID: {id} olan etkinlik bulunamadı.");
            }

            await _db.DeleteSpecialEventAsync(id);

            return NoContent();
        }
    }
}

