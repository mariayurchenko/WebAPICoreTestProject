using System.Threading.Tasks;
using App.Repository;
using Core.Models;

namespace App.ApplicationLogic
{
    public class TicketScreenUseCases : ITicketScreenUseCases
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketScreenUseCases( ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<int> AddTickets(Ticket ticket)
        {
            return await _ticketRepository.CreateAsync(ticket);
        }

        public async Task DeleteTicket(int id)
        {
            await _ticketRepository.DeleteAsync(id);
        }
    }
}