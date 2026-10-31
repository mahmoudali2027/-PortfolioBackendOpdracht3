
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public class ProjectService : IProjectService
{
    private readonly PortfolioDbContext _db;

    public ProjectService(PortfolioDbContext db)
    {
        _db = db;
    }

    // 1. Bütün projeleri getir (GET)
    public async Task<List<Project>> GetAllAsync()
    {
        return await _db.Projects
            .AsNoTracking()
            .ToListAsync();
    }

    // 2. ID ile bir proje getir (GET by ID)
    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _db.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // 3. Yeni proje ekle (POST)
    public async Task<Project> CreateAsync(Project project)
    {
        _db.Projects.Add(project);
        await _db.SaveChangesAsync();

        return project;
    }

    // 4. Projeyi güncelle (PUT)
    public async Task<bool> UpdateAsync(int id, Project updatedProject)
    {
        var project = await _db.Projects.FindAsync(id);

        if (project == null)
        {
            return false;
        }

        project.Titel = updatedProject.Titel;
        project.Beschrijving = updatedProject.Beschrijving;
        project.Categorie = updatedProject.Categorie;
        project.GitHubUrl = updatedProject.GitHubUrl;
        project.Datum = updatedProject.Datum;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
{
    var project = await _db.Projects.FindAsync(id);

    if (project == null)
    {
        return false;
    }

    _db.Projects.Remove(project);
    await _db.SaveChangesAsync();

    return true;
}
}