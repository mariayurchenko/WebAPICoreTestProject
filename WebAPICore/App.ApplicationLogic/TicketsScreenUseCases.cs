using System.Collections.Generic;
using System.Threading.Tasks;
using App.Repository;
using Core.Models;

namespace App.ApplicationLogic
{
    public class TicketsScreenUseCases : ITicketsScreenUseCases
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketsScreenUseCases(IProjectRepository projectRepository, ITicketRepository ticketRepository)
        {
            _projectRepository = projectRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> ViewTickets(int projectId)
        {
            return await _projectRepository.GetProjectTicketsAsync(projectId);
        }

        public async Task<IEnumerable<Ticket>> SearchTickets(string filter)
        {
            if (int.TryParse(filter, out var ticketId))
            {
                var ticket = await _ticketRepository.GetByIdAsync(ticketId);

                var tickets = new List<Ticket>();
                tickets.Add(ticket);

                return tickets;
            }

            return await _ticketRepository.GetAsync(filter);
        }

        public async Task<IEnumerable<Ticket>> ViewOwnersTickets(int projectId, string ownerName)
        {
            return await _projectRepository.GetProjectTicketsAsync(projectId, ownerName);
        }

        public async Task<Ticket> ViewTicketById(int id)
        {
            return await _ticketRepository.GetByIdAsync(id);
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            await _ticketRepository.UpdateAsync(ticket);
        }
    }
}