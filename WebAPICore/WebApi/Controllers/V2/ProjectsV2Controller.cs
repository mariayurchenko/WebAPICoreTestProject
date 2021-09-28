using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;
using WebApi.QueryFilters;

namespace WebApi.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/projects")]
    //[CustomTokenAuthFilter]
    [Authorize]
    public class ProjectsV2Controller : ControllerBase
    {
        private readonly BugsContext _db;
        public ProjectsV2Controller(BugsContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Projects.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpGet]
        [Route("/api/projects/{pid:int}/tickets")]
        public async Task<IActionResult> GetProjectTickets(int pId,
            [FromQuery] ProjectQueryFilter filter)
        {
            IQueryable<Ticket> tickets = _db.Tickets.Where(t => t.ProjectId == pId);
            if (filter != null && !string.IsNullOrWhiteSpace(filter.Owner))
                tickets = tickets.Where(t => !string.IsNullOrWhiteSpace(t.Owner) &&
                                             t.Owner.ToLower() == filter.Owner.ToLower());

            var listTickets = await tickets.ToListAsync();
            if (listTickets == null || listTickets.Count <= 0)
                return NotFound();

            return Ok(listTickets);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();

            return CreatedAtAction( nameof(GetById), new {id = project.ProjectId}, project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            try
            {
                _db.Entry(project).State = EntityState.Modified;

                await _db.SaveChangesAsync();
            }
            catch
            {
                if (await _db.Projects.FindAsync(id) == null)
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
            var project = await _db.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();

            return Ok(project);
        }
    }
}