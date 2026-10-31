using PortfolioApi.Models;

namespace PortfolioApi.Services;

public interface IBlogpostService
{
    Task<List<Blogpost>> GetAllAsync();
    Task<Blogpost?> GetByIdAsync(int id);
    Task<Blogpost> CreateAsync(Blogpost blogpost);
    Task<bool> UpdateAsync(int id, Blogpost updatedBlogpost);
    Task<bool> DeleteAsync(int id);
}