
using PortfolioApi.DTOs.Blogposts;
using PortfolioApi.Models;
using PortfolioApi.Services;

namespace PortfolioApi.Endpoints;

public static class BlogpostEndpoints
{
    public static void MapBlogpostEndpoints(this WebApplication app)
    {
        var blogposts = app.MapGroup("/api/blogposts");

        // 1. Bütün blog yazılarını getir
        blogposts.MapGet("/", async (IBlogpostService service) =>
        {
            var result = await service.GetAllAsync();

            var response = result.Select(b => new BlogpostResponse
            {
                Id = b.Id,
                Titel = b.Titel,
                Inhoud = b.Inhoud,
                Publicatiedatum = b.Publicatiedatum
            }).ToList();

            return Results.Ok(response);
        });

        // 2. ID ile tek blog yazısı getir
        blogposts.MapGet("/{id:int}", async (int id, IBlogpostService service) =>
        {
            var blogpost = await service.GetByIdAsync(id);

            if (blogpost == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new BlogpostResponse
            {
                Id = blogpost.Id,
                Titel = blogpost.Titel,
                Inhoud = blogpost.Inhoud,
                Publicatiedatum = blogpost.Publicatiedatum
            });
        });

        // 3. Yeni blog yazısı ekle
        blogposts.MapPost("/", async (
            CreateBlogpostRequest request,
            IBlogpostService service) =>
        {
            var blogpost = new Blogpost
            {
                Titel = request.Titel,
                Inhoud = request.Inhoud,
                Publicatiedatum = request.Publicatiedatum!.Value
            };

            var created = await service.CreateAsync(blogpost);

            var response = new BlogpostResponse
            {
                Id = created.Id,
                Titel = created.Titel,
                Inhoud = created.Inhoud,
                Publicatiedatum = created.Publicatiedatum
            };

            return Results.Created($"/api/blogposts/{created.Id}", response);
        });

        // 4. Blog yazısını güncelle
        blogposts.MapPut("/{id:int}", async (
            int id,
            UpdateBlogpostRequest request,
            IBlogpostService service) =>
        {
            var updatedBlogpost = new Blogpost
            {
                Titel = request.Titel,
                Inhoud = request.Inhoud,
                Publicatiedatum = request.Publicatiedatum!.Value
            };

            var updated = await service.UpdateAsync(id, updatedBlogpost);

            return updated
                ? Results.NoContent()
                : Results.NotFound();
        });

        // 5. Blog yazısını sil
        blogposts.MapDelete("/{id:int}", async (
            int id,
            IBlogpostService service) =>
        {
            var deleted = await service.DeleteAsync(id);

            return deleted
                ? Results.NoContent()
                : Results.NotFound();
        });
    }
}
