using System.Collections.Generic;
using System.Threading.Tasks;
using App.Repository.ApiClient;
using Core.Models;

namespace App.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;

        public TicketRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Ticket>> GetAsync(string filter = null)
        {
            var uri = "api/tickets?api-version=2.0";

            if (!string.IsNullOrWhiteSpace(filter))
                uri += $"&titleordescription={filter.Trim()}";

            return await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _webApiExecuter.InvokeGet<Ticket>($"api/tickets/{id}?api-version=2.0");
        }

        public async Task<int> CreateAsync(Ticket ticket)
        {
            ticket = await _webApiExecuter.InvokePost("api/tickets?api-version=2.0", ticket);
            return ticket.TicketId.Value;
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            await _webApiExecuter.InvokePut($"api/tickets/{ticket.TicketId}?api-version=2.0", ticket);
        }

        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete($"api/tickets/{id}?api-version=2.0");
        }
    }
}