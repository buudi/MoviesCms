
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using Movies.Cms.Services;
using Movies.Cms.Controllers.Models;
using Asp.Versioning;

namespace Movies.Cms.Controllers;

[ApiVersion(1)]
[ApiExplorerSettings(GroupName = "Movies V1")]
[Route("api/v1/en/[controller]")]
[ApiController]
public class MovieController(MovieService movieService):UmbracoApiController
{
    [HttpGet("movies")]
    public IActionResult GetMovies()
    {
        var movies = movieService.AllMovies;
        return Ok(movies);
    }

    [HttpGet("id")]
    public IActionResult GetId(Guid Id)
    {
        var movie = movieService.getMovieById(Id);

        return Ok(movie);
    }

    [HttpPost("movies")]
    public IActionResult CreateMovie(CreateMovieRequest requestModel)
    {
        Console.WriteLine("CreateMovie at controller runs okay");

	    movieService.CreateMovie("en-US", requestModel.Name, requestModel.Synopsis, requestModel.ReleaseYear, requestModel.Director, requestModel.Poster);
        return Ok(movieService.AllMovies);
    }
        
    [HttpPut("movies")]
    public IActionResult UpdateMovie(Guid id, UpdateMovieRequest requestModel)
    {
        movieService.UpdateMovie("en-US", id, requestModel.Name, requestModel.Synopsis, requestModel.ReleaseYear, requestModel.Director, requestModel.Poster);
        return Ok(movieService.AllMovies);
    }

    [HttpDelete("movies")]
    public IActionResult DeleteMovie(Guid id)
    {
        movieService.DeleteMovie(id);
        return Ok("Deleted");
    }
}