using Microsoft.AspNetCore.Mvc;
using Movies.Cms.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Cms.Controllers.Models;

public class UpdateMovieRequest
{
	[Required]
	public Guid Id { get; set; }

	[FromQuery]
	public string? culture { get; set; }

	[Required]
	public string? Name { get; set; }

	[Required]
	public string? Synopsis { get; set; }

	[Required]
	public DateTime? ReleaseYear { get; set; }

	[Required]
	public Guid Director { get; set; }
	
	[Required]
	public IFormFile? Poster { get; set; }

}
                               