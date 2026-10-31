using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.DTOs.Blogposts;

public class CreateBlogpostRequest
{
    [Required]
    [StringLength(200)]
    public string Titel { get; set; } = "";

    [Required]
    [StringLength(20000)]
    public string Inhoud { get; set; } = "";

    [Required]
    public DateTime? Publicatiedatum { get; set; }
}

public class UpdateBlogpostRequest : CreateBlogpostRequest
{
}

public class BlogpostResponse
{
    public int Id { get; set; }
    public string Titel { get; set; } = "";
    public string Inhoud { get; set; } = "";
    public DateTime Publicatiedatum { get; set; }
}