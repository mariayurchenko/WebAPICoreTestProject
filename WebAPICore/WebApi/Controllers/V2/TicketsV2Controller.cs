using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;
using WebApi.Filters.V2;
using WebApi.QueryFilters;

namespace WebApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]
    //[CustomTokenAuthFilter]
    [Authorize]
    public class TicketsV2Controller : ControllerBase
    {
        private readonly BugsContext _db;
        public TicketsV2Controller(BugsContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketQueryFilter ticketQueryFilter)
        {
            IQueryable<Ticket> tickets = _db.Tickets;

            if (ticketQueryFilter != null)
            {
                if(ticketQueryFilter.Id.HasValue)
                    tickets = tickets.Where(x => x.TicketId == ticketQueryFilter.Id);

                if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Title))
                    tickets = tickets.Where(x => x.Title.Contains(ticketQueryFilter.Title, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Description))
                    tickets = tickets.Where(x => x.Description.Contains(ticketQueryFilter.Description, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(await tickets.ToListAsync());
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
        [Ticket_EnsureDescriptionPresentActionFilter]
        public async Task<IActionResult> Post(Ticket ticket)
        {
            await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId }, ticket);
        }

        [HttpPut("{id}")]
        [Ticket_EnsureDescriptionPresentActionFilter]
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