using Movies.Cms.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Cms.Controllers.Models;

public class UpdateMovieRequest
{
	[Required]
	public string? Name { get; set; }

	[Required]
	public string? Synopsis { get; set; }

	[Required]
	public DateTime? ReleaseYear { get; set; }

	[Required]
	public Guid Director { get; set; }
}
                               