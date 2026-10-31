using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public class BlogpostService : IBlogpostService
{
    private readonly PortfolioDbContext _db;

    public BlogpostService(PortfolioDbContext db)
    {
        _db = db;
    }

    // Bütün blog yazılarını getir
    public async Task<List<Blogpost>> GetAllAsync()
    {
        return await _db.Blogposts
            .AsNoTracking()
            .ToListAsync();
    }

    // ID ile tek blog yazısı getir
    public async Task<Blogpost?> GetByIdAsync(int id)
    {
        return await _db.Blogposts
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    // Yeni blog yazısı ekle
    public async Task<Blogpost> CreateAsync(Blogpost blogpost)
    {
        _db.Blogposts.Add(blogpost);
        await _db.SaveChangesAsync();

        return blogpost;
    }

    // Blog yazısını güncelle
    public async Task<bool> UpdateAsync(int id, Blogpost updatedBlogpost)
    {
        var blogpost = await _db.Blogposts.FindAsync(id);

        if (blogpost == null)
        {
            return false;
        }

        blogpost.Titel = updatedBlogpost.Titel;
        blogpost.Inhoud = updatedBlogpost.Inhoud;
        blogpost.Publicatiedatum = updatedBlogpost.Publicatiedatum;

        await _db.SaveChangesAsync();
        return true;
    }

    // Blog yazısını sil
    public async Task<bool> DeleteAsync(int id)
    {
        var blogpost = await _db.Blogposts.FindAsync(id);

        if (blogpost == null)
        {
            return false;
        }

        _db.Blogposts.Remove(blogpost);
        await _db.SaveChangesAsync();

        return true;
    }
}