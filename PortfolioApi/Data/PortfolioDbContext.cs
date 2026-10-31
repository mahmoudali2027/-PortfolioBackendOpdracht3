using Microsoft.EntityFrameworkCore;
using PortfolioApi.Models;

namespace PortfolioApi.Data;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(
        DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Blogpost> Blogposts => Set<Blogpost>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(p => p.Titel)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(p => p.Beschrijving)
                .HasMaxLength(4000)
                .IsRequired();

            entity.Property(p => p.Categorie)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.GitHubUrl)
                .HasMaxLength(2048)
                .IsRequired();
        });

        modelBuilder.Entity<Blogpost>(entity =>
        {
            entity.Property(b => b.Titel)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(b => b.Inhoud)
                .HasMaxLength(20000)
                .IsRequired();
        });
    }
}