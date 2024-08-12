
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using Movies.Cms.Services;
using Movies.Cms.Controllers.Models;

namespace Movies.Cms.Controllers;

[Route("api/v1/en")]
[ApiController]
public class MyController(MovieService movieService):UmbracoApiController
{
    [HttpGet("movies")]
    public IActionResult GetMovies()
    {
        var movies = movieService.AllMovies;
        return Ok(movies);
    }

    [HttpPost("movies")]
    public IActionResult CreateMovie(CreateMovieRequest requestModel)
    {
	movieService.CreateMovie("en-US", requestModel.Name, requestModel.Synopsis, requestModel.ReleaseYear, requestModel.Director);
        return Ok(movieService.AllMovies);
    }
}