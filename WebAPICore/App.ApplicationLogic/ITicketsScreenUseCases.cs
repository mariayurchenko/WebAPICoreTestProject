using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace App.ApplicationLogic
{
    public interface ITicketsScreenUseCases
    {
        Task<IEnumerable<Ticket>> ViewTickets(int projectId);
        Task<IEnumerable<Ticket>> SearchTickets(string filter);
        Task<IEnumerable<Ticket>> ViewOwnersTickets(int projectId, string ownerName);
        Task<Ticket> ViewTicketById(int id);
        Task UpdateTicket(Ticket ticket);
    }
}