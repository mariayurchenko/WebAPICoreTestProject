using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace App.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAsync();
        Task<Project> GetByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId, string filter = null);
        Task<int> CreateAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
    }
}