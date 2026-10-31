using PortfolioApi.Models;

namespace PortfolioApi.Services;

public interface IProjectService
{
    Task<List<Project>> GetAllAsync();
      Task<Project?> GetByIdAsync(int id);
      Task<Project> CreateAsync(Project project);
      Task<bool> UpdateAsync(int id, Project updatedProject);
      Task<bool> DeleteAsync(int id);
}