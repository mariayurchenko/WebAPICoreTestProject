using System.Threading.Tasks;
using Core.Models;

namespace App.ApplicationLogic
{
    public interface ITicketScreenUseCases
    {
        Task<int> AddTickets(Ticket ticket);
        Task DeleteTicket(int id);
    }
}