

using PortfolioApi.DTOs.Projects;
using PortfolioApi.Models;
using PortfolioApi.Services;

namespace PortfolioApi.Endpoints;

public static class ProjectEndpoints
{
    public static void MapProjectEndpoints(this WebApplication app)
    {
        var projects = app.MapGroup("/api/projects");

        // 1. Bütün projeleri getir
        projects.MapGet("/", async (IProjectService service) =>
        {
            var result = await service.GetAllAsync();

            var response = result.Select(p => new ProjectResponse
            {
                Id = p.Id,
                Titel = p.Titel,
                Beschrijving = p.Beschrijving,
                Categorie = p.Categorie,
                GitHubUrl = p.GitHubUrl,
                Datum = p.Datum
            }).ToList();

            return Results.Ok(response);
        });

        // 2. ID ile tek proje getir
        projects.MapGet("/{id:int}", async (int id, IProjectService service) =>
        {
            var project = await service.GetByIdAsync(id);

            if (project == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new ProjectResponse
            {
                Id = project.Id,
                Titel = project.Titel,
                Beschrijving = project.Beschrijving,
                Categorie = project.Categorie,
                GitHubUrl = project.GitHubUrl,
                Datum = project.Datum
            });
        });

        // 3. Yeni proje ekle
        projects.MapPost("/", async (CreateProjectRequest request, IProjectService service) =>
        {
            var project = new Project
            {
                Titel = request.Titel,
                Beschrijving = request.Beschrijving,
                Categorie = request.Categorie,
                GitHubUrl = request.GitHubUrl,
                Datum = request.Datum!.Value
            };

            var created = await service.CreateAsync(project);

            var response = new ProjectResponse
            {
                Id = created.Id,
                Titel = created.Titel,
                Beschrijving = created.Beschrijving,
                Categorie = created.Categorie,
                GitHubUrl = created.GitHubUrl,
                Datum = created.Datum
            };

            return Results.Created($"/api/projects/{created.Id}", response);
        });

        // 4. Mevcut projeyi güncelle
        projects.MapPut("/{id:int}", async (
            int id,
            UpdateProjectRequest request,
            IProjectService service) =>
        {
            var updatedProject = new Project
            {
                Titel = request.Titel,
                Beschrijving = request.Beschrijving,
                Categorie = request.Categorie,
                GitHubUrl = request.GitHubUrl,
                Datum = request.Datum!.Value
            };

            var updated = await service.UpdateAsync(id, updatedProject);

            return updated
                ? Results.NoContent()
                : Results.NotFound();
        });

        // 5. Projeyi sil
        projects.MapDelete("/{id:int}", async (int id, IProjectService service) =>
        {
            var deleted = await service.DeleteAsync(id);

            return deleted
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}