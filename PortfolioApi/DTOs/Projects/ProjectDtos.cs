using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.DTOs.Projects;

public class CreateProjectRequest
{
    [Required, StringLength(200)] public string Titel { get; set; } = "";
    [Required, StringLength(4000)] public string Beschrijving { get; set; } = "";
    [Required, StringLength(100)] public string Categorie { get; set; } = "";
    [Required, Url, StringLength(2048)] public string GitHubUrl { get; set; } = "";
    [Required] public DateTime? Datum { get; set; }
}

public class UpdateProjectRequest : CreateProjectRequest
{
}

public class ProjectResponse
{
    public int Id { get; set; }
    public string Titel { get; set; } = "";
    public string Beschrijving { get; set; } = "";
    public string Categorie { get; set; } = "";
    public string GitHubUrl { get; set; } = "";
    public DateTime Datum { get; set; }
}
