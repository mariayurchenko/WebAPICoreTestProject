using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace App.Repository
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAsync(string filter = null);
        Task<Ticket> GetByIdAsync(int id);
        Task<int> CreateAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(int id);
    }
}