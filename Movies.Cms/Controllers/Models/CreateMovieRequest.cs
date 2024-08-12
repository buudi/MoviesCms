using Movies.Cms.Models;
using System.ComponentModel.DataAnnotations;
using Umbraco.Cms.Core.Models;

namespace Movies.Cms.Controllers.Models;

public class CreateMovieRequest
{

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Synopsis { get; set; }

    [Required]
    public DateTime? ReleaseYear { get; set; }

    [Required]
    public Director? Director { get; set; }
}



