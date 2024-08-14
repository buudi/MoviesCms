using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Movies.Cms.Services;
using Umbraco.Cms.Web.Common.Controllers;

namespace Movies.Cms.Controllers;

[ApiVersion(1)]
[ApiExplorerSettings(GroupName = "Directors V1")]
[Route("api/v1/[controller]")]
[ApiController]
public class DirectorController(DirectorService directorService) : UmbracoApiController
{
	[HttpGet]
	public IActionResult GetDirectors([FromQuery]string? culture)
	{
		culture = culture ?? "en-US";	
		var directors = directorService.AllDirectors(culture);
		return Ok(directors);
	}
}
