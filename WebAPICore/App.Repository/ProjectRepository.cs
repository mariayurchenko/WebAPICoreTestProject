using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Repository.ApiClient;
using Core.Models;

namespace App.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;

        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Project>> GetAsync()
        {
            return await _webApiExecuter.InvokeGet<IEnumerable<Project>>("api/projects");
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _webApiExecuter.InvokeGet<Project>($"api/projects/{id}");
        }

        public async Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId, string filter = null)
        {
            var uri = $"api/projects/{projectId}/tickets";

            if (!string.IsNullOrWhiteSpace(filter))
            {
                uri += $"?Owner={filter}&api-version=2.0";
            }

            return await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }

        public async Task<int> CreateAsync(Project project)
        {
            project = await _webApiExecuter.InvokePost("api/projects", project);

            if (project.ProjectId == null)
            {
                throw new Exception($"{nameof(project.ProjectId)} is null");
            }

            return project.ProjectId.Value;
        }

        public async Task UpdateAsync(Project project)
        {
            await _webApiExecuter.InvokePut($"api/projects/{project.ProjectId}", project);
        }

        public async Task DeleteAsync(int id)
        {
            await _webApiExecuter.InvokeDelete($"api/projects/{id}");
        }
    }
}