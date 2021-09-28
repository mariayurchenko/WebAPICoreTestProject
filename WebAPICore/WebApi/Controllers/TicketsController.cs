using System.Threading.Tasks;
using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    //[CustomTokenAuthFilter]
    [Authorize(policy: "WebApiWriteScope")]
    public class TicketsController : ControllerBase
    {
        private readonly BugsContext _db;
        public TicketsController(BugsContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Tickets.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _db.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Ticket ticket)
        {
            await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            try
            {
                _db.Entry(ticket).State = EntityState.Modified;

                await _db.SaveChangesAsync();
            }
            catch
            {
                if (await _db.Tickets.FindAsync(id) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _db.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _db.Tickets.Remove(ticket);
            await _db.SaveChangesAsync();

            return Ok(ticket);
        }
    }
}