using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace App.ApplicationLogic
{
    public interface IProjectScreenUseCases
    {
        Task<IEnumerable<Project>> ViewProjects();
    }
}