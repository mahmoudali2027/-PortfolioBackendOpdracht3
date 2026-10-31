
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Endpoints;
using PortfolioApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Swagger ve API doğrulama
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidation();

// SQL Server bağlantısı
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("PortfolioDb")));

// Dependency Injection
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IBlogpostService, BlogpostService>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Minimal API endpointleri
app.MapProjectEndpoints();
app.MapBlogpostEndpoints();

app.Run();
