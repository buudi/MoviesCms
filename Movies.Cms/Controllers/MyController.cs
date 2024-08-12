
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using Movies.Cms.Services;
using Movies.Cms.Controllers.Models;
using Smidge.Models;

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
	    movieService.CreateMovie("en-US", requestModel.Name, requestModel.Synopsis, requestModel.ReleaseYear, requestModel.DirectorId);

        return Ok(movieService.AllMovies);
    }
        
    [HttpPut("movies")]
    public IActionResult UpdateMovie(Guid id, UpdateMovieRequest requestModel)
    {
        movieService.UpdateMovie("en-US", id, requestModel.Name, requestModel.Synopsis, requestModel.ReleaseYear, requestModel.Director);
        return Ok(movieService.AllMovies);
    }
}