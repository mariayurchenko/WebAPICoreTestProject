using System.Collections.Generic;
using System.Threading.Tasks;
using App.Repository;
using Core.Models;

namespace App.ApplicationLogic
{
    public class ProjectScreenUseCases : IProjectScreenUseCases
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectScreenUseCases(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> ViewProjects()
        {
            return await _projectRepository.GetAsync();
        }
    }
}